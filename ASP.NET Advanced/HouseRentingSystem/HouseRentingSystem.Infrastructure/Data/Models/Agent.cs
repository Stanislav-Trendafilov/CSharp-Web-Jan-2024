﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Data.DataConstants;
namespace HouseRentingSystem.Data.Models
{
    [Index(nameof(PhoneNumber),IsUnique=true)]
    public class Agent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AgentPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

    }

}
