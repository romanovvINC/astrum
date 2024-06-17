﻿using System.ComponentModel.DataAnnotations;
using Astrum.CodeRev.Domain.Aggregates.Enums;

namespace Astrum.CodeRev.Application.UserService.ViewModel.DTO.Tasks;

public class TaskCreationDto
{
    [Required]
    public string TaskText { get; set; }
    [Required]
    public string StartCode { get; set; }
    [Required]
    public string Name { get; set; }
    public string TestsCode { get; set; }
    [Required]
    public int RunAttempts { get; set; }
    [Required]
    public ProgrammingLanguage ProgrammingLanguage { get; set; }
}