﻿
using Astrum.CodeRev.Domain.Aggregates.Enums;

namespace Astrum.CodeRev.Application.UserService.ViewModel.DTO.Review;

public class CardInfo
{
    public string Username { get; set; }
    public Guid InterviewSolutionId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Vacancy { get; set; }
    public long StartTimeMs { get; set; }
    public long EndTimeMs { get; set; }
    public long TimeToCheckMs { get; set; }
    public Grade AverageGrade { get; set; }
    public string ReviewerComment { get; set; }
    public int DoneTasksCount { get; set; }
    public int TasksCount { get; set; }
    public InterviewResult InterviewResult { get; set; }
    public bool IsSubmittedByCandidate { get; set; }
    public bool IsSolutionTimeExpired { get; set; }
    public bool HasReviewerCheckResult { get; set; }
    public bool HasHrCheckResult { get; set; }
    public IList<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public bool IsSynchronous { get; set; }
}