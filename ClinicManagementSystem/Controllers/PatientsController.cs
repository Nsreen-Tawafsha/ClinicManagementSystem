using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ClinicManagementSystem.Paging;

namespace ClinicManagementSystem.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task <IActionResult> Index(string sortField, string currentSortField, string currentSortOrder, string SearchString, string currentFilter, int? pageNo)

        {
            List<Patient> patients = this._context.Patients.ToList();
            if (SearchString != null)
            {
                pageNo = 1;
            }
            else
            {
                SearchString = currentFilter;
            } 
            ViewData["CurrentSort"] = sortField;
            ViewBag.CurrentFilter = SearchString;

            if (!String.IsNullOrEmpty(SearchString))
            {
                patients = patients.Where(s => s.PatientName.Contains(SearchString)).ToList();
                return View(this.SortData(patients, sortField, currentSortField, currentSortOrder));
            }
            patients = this.SortData(patients, sortField, currentSortField, currentSortOrder);
            int pageSize = 8;
            return View(PagingList<Patient>.CreateAsync(patients.AsQueryable<Patient>(), pageNo ?? 1, pageSize));
        }


        private List<Patient> SortData(List<Patient> patients, string sortField, string currentSortField, string currentSortOrder)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "PatientName";
                ViewBag.SortOrder = "Asc";
            }
            else
            {
                if (currentSortField == sortField)
                {
                    ViewBag.SortOrder = currentSortOrder == "Asc" ? "Desc" : "Asc";
                }
                else
                {
                    ViewBag.SortOrder = "Asc";
                }
                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(Patient).GetProperty(ViewBag.SortField);
            if (ViewBag.SortOrder == "Asc")
            {
                patients = patients.OrderBy(s => propertyInfo.GetValue(s, null)).ToList();
            }
            else
            {
                patients = patients.OrderByDescending(s => propertyInfo.GetValue(s, null)).ToList();
            }
            return patients;
        }



        // GET: Patients/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public async Task< IActionResult> Create()
        {
            ViewData["Country"] = new SelectList(await this.getCountries(), "Name", "Name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Birthday,Gender,PhoneNumber,Email,Address,RegistrationDate,SSN,Country")] Patient patient, CountryModel country)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(await this.getCountries(), "Name", "Name", country.Name);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["Country"] = new SelectList(await this.getCountries(), "Name", "Name");
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FirstName,LastName,Birthday,Gender,PhoneNumber,Email,Address,RegistrationDate,SSN,Country")] Patient patient, CountryModel country)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Country"] = new SelectList(await this.getCountries(), "Name", "Name", country.Name);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(long id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }


        //----------------------------------------------------------------------------
        //-------------------- to get the countries list
        
        public async Task<IEnumerable<CountryModel>> getCountries()
        {
            string Baseurl = "https://restcountries.eu/rest/v2/all";
            List<CountryModel> country = new List<CountryModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("https://restcountries.eu/rest/v2/all");

                if (Res.IsSuccessStatusCode)
                {
                    var CountryResponse = Res.Content.ReadAsStringAsync().Result;
                    country = JsonConvert.DeserializeObject<List<CountryModel>>(CountryResponse);
                }

                return (country);
            }

        }


    }
}
