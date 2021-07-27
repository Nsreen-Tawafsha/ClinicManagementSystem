using System;

namespace ClinicManagementSystem.Controllers
{
    public class CountryModel
    {
        public CountryModel()
        {
       
        }

        public CountryModel(string name)
        {
          
            Name = name ?? throw new ArgumentNullException(nameof(name));
      
        }

        //country name
        public string Name { get; set; }
    }

}