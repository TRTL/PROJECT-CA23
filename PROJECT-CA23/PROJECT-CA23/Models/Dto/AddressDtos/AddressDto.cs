﻿using System.ComponentModel.DataAnnotations;
using PROJECT_CA23.Models.Dto.UserDtos;

namespace PROJECT_CA23.Models.Dto.AddressDto
{
    public class AddressDto
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string AddressText { get; set; }
        public string PostCode { get; set; }
    }
}
