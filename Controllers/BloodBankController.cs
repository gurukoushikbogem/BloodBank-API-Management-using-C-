using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BloodBank.Models;
using System.Reflection;

namespace BloodBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBankController : ControllerBase
    {

        static List<BloodBankModel> ListOfDonors = new List<BloodBankModel>
        {
            new BloodBankModel
            {
                Id = 1,
                DonorName = "Koushik",
                Age = 22,
                BloodType = "O+",
                MobileNo = 0123456789,
                Email = "koushik@email.com",
                QuantityInMl = 450,
                CollectionDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30),
                Status = "Available"
            },
            new BloodBankModel
            {
                Id = 2,
                DonorName = "Rahul",
                Age = 22,
                BloodType = "A+",
                MobileNo = 1234567890,
                Email = "rahul@email.com",
                QuantityInMl = 3000,
                CollectionDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30),
                Status = "Available"
            },
            new BloodBankModel
            {
                Id = 3,
                DonorName = "manohar",
                Age = 75,
                BloodType = "B+",
                MobileNo = 555555555,
                Email = "manohar@email.com",
                QuantityInMl = 500,
                CollectionDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(30),
                Status = "Expired"
            }
        };

        // Retrieving all entries in blood bank list
        [HttpGet]
        public ActionResult<IEnumerable<BloodBankModel>> FetchAllDonors()
        {
            if(ListOfDonors.Count == 0)
            {
                return NotFound();
            }
            return ListOfDonors;
        }

        // Retrieving a specific entry from blood bank list
        [HttpGet("{id}")]
        public ActionResult<BloodBankModel> GetById(int id)
        {
            var donor = ListOfDonors.Find(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            return donor;
        }

        // Adding a new entry to blood bank list
        [HttpPost]
        public ActionResult<BloodBankModel> AddEntry(BloodBankModel model)
        {
            if(model.DonorName == null || model.BloodType == null || model.MobileNo == 0 || model.Email == null || model.QuantityInMl == 0 || model.Status == null)
            {
                return BadRequest("Please fill all the fields");
            }
            if(model.MobileNo.ToString().Length != 10)
            {
                return BadRequest("Mobile number should be of 10 digits");
            }
            if(model.QuantityInMl < 200)
            {
                return BadRequest("Minimum quantity should be 200 ml");
            }
            var status = new HashSet<string> { "Available", "Requested", "Expired" };
            if (!status.Contains(model.Status))
            {
                return BadRequest("Invalid Status . Valid Status (Available, Requested, Expired)");
            }
            model.Id = ListOfDonors.Count + 1;
            model.CollectionDate= DateTime.Now;
            model.ExpirationDate = DateTime.Now.AddDays(30);
            ListOfDonors.Add(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // Updating an existing entry in blood bank list
        [HttpPut("{id}")]
        public IActionResult UpdateEntry(int id, BloodBankModel model)
        {
            var donor = ListOfDonors.Find(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            if (model.DonorName == null || model.BloodType == null || model.MobileNo == 0 || model.Email == null || model.QuantityInMl == 0 || model.Status == null)
            {
                return BadRequest("Please fill all the fields");
            }
            if (model.MobileNo.ToString().Length != 10)
            {
                return BadRequest("Mobile number should be of 10 digits");
            }
            if (model.QuantityInMl < 200)
            {
                return BadRequest("Minimum quantity should be 200 ml");
            }
            var status = new HashSet<string> { "Available", "Requested", "Expired" };
            if (!status.Contains(model.Status))
            {
                return BadRequest("Invalid Status . Valid Status (Available, Requested, Expired)");
            }
            donor.DonorName = model.DonorName;
            donor.Age = model.Age;
            donor.BloodType = model.BloodType;
            donor.MobileNo = model.MobileNo;
            donor.Email = model.Email;
            donor.QuantityInMl = model.QuantityInMl;
            donor.CollectionDate = model.CollectionDate;
            donor.ExpirationDate = model.ExpirationDate;
            donor.Status = model.Status;
            return NoContent();
        }

        // Deleting an entry from blood bank list
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var donor = ListOfDonors.Find(d => d.Id == id);
            if (donor == null)
            {
                return NotFound();
            }
            ListOfDonors.Remove(donor);
            return NoContent();
        }

        // Pagination
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<BloodBankModel>> GetPaginatedList(int pageNumber=1, int pageSize=3)
        {
            var paginatedList = ListOfDonors.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return paginatedList;
        }

        // Searching for particular blood type
        [HttpGet("bloodtype")]
        public ActionResult<IEnumerable<BloodBankModel>> GetByBloodType(string bloodType)
        {
            var donors = ListOfDonors.FindAll(d => d.BloodType == bloodType);
            if (donors == null)
            {
                return NotFound();
            }
            return donors;
        }

        // Searching for particular status
        [HttpGet("status")]
        public ActionResult<IEnumerable<BloodBankModel>> GetByStatus(string status)
        {
            var blood_status = new HashSet<string> { "Available", "Requested", "Expired" };
            if (!blood_status.Contains(status))
            {
                return BadRequest("Invalid Status . Valid Status (Available, Requested, Expired)");
            }
            var donors = ListOfDonors.FindAll(d => d.Status == status);
            if (donors == null)
            {
                return NotFound();
            }
            return donors;
        }

        // Searching for particular donor name
        [HttpGet("donorname")]
        public ActionResult<IEnumerable<BloodBankModel>> GetByDonorName(string donorName)
        {
            var donors = ListOfDonors.FindAll(d => d.DonorName == donorName);
            if (donors == null)
            {
                return NotFound();
            }
            return donors;
        }

        // Sorting by Blood Type
        [HttpGet("sortbloodtype")]
        public ActionResult<IEnumerable<BloodBankModel>> SortByBloodType(string sortorder="asc")
        {
            var res = ListOfDonors.AsQueryable();
            res = sortorder.ToLower() == "asc" ? res.OrderBy(x => x.BloodType) : res.OrderByDescending(x => x.BloodType);
            return res.ToList();
        }

        // Sorting by Collection Date
        [HttpGet("sortcollectiondate")]
        public ActionResult<IEnumerable<BloodBankModel>> SortByCollectionDate(string sortorder = "asc")
        {
            var res = ListOfDonors.AsQueryable();
            res = sortorder.ToLower() == "asc" ? res.OrderBy(x => x.CollectionDate) : res.OrderByDescending(x => x.CollectionDate);
            return res.ToList();
        }

        // filtering based on blood and status
        [HttpGet("filter")]
        public ActionResult<IEnumerable<BloodBankModel>> FilterByBloodTypeAndStatus(string bloodType, string status)
        {
            var status_blood = new HashSet<string> { "Available", "Requested", "Expired" };
            if (!status_blood.Contains(status))
            {
                return BadRequest("Invalid Status . Valid Status (Available, Requested, Expired)");
            }
            var donors = ListOfDonors.FindAll(d => d.BloodType == bloodType && d.Status == status);
            if (donors == null)
            {
                return NotFound();
            }
            return donors;
        }
    }
}
