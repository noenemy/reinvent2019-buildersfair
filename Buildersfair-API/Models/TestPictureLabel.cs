using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildersFair_API.Models
{
    [Table("tb_test_picture_labels")]
    public class TestPictureLabel
    {
        public int picture_id { get; set; }
        public string label_name { get; set; }
        public double confidence { get; set; }     
    }
}