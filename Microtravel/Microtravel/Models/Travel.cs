using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Microtravel.Models
{
    public class Travel
    {
        public int Id { get; set; }

        public string? TravelIdentifier { get; set; }

        public string Name { get; set; }

        public TravelType? TravelType { get; set; }

        public int TravelTypeId { get; set; }

        public TravelDealType? TravelDealType  { get; set; }

        public int TravelDealTypeId { get; set; }

        public string? Description { get; set; }

        public int Price { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile? travelPicture { get; set; }
        public string? travelPictureUrl { get; set; }
        /*
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd.}", ApplyFormatInEditMode = false)]
        */
        public DateTime TravelDate { get; set; }
        public DateTime? TravelRegDate { get; set; }

        public int? Enabled { get; set; }


    }
}
