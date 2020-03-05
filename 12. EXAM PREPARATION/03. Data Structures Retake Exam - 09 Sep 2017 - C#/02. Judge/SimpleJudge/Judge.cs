using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    private readonly HashSet<int> _users;
    private readonly HashSet<int> _contests;
    private readonly Dictionary<int, Submission> _bySubmissionsId;

    public Judge()
    {
        this._users = new HashSet<int>();
        this._contests = new HashSet<int>();
        this._bySubmissionsId = new Dictionary<int, Submission>();
    }

    public void AddContest(int contestId)
    {
        this._contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (!this._users.Contains(submission.UserId) ||
            !this._contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }

        if (this._bySubmissionsId.ContainsKey(submission.Id))
        {
            //this._bySubmissionsId[submission.Id] = submission;
            return;
        }

        this._bySubmissionsId.Add(submission.Id, submission);
    }

    public void AddUser(int userId)
    {
        this._users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this._bySubmissionsId.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }
        
        var submission = this._bySubmissionsId[submissionId];
        this._bySubmissionsId.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this._bySubmissionsId.Values
            .OrderBy(x => x.Id);
    }

    public IEnumerable<int> GetUsers()
    {
        return this._users
            .OrderBy(x => x);
    }

    public IEnumerable<int> GetContests()
    {
        return this._contests
            .OrderBy(x => x);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this._bySubmissionsId.Values
            .Where(x => x.Type == submissionType &&
                                 x.Points >= minPoints && 
                                 x.Points <= maxPoints);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        if (!this._users.Contains(userId))
        {
            throw new InvalidOperationException();
        }

        return this._bySubmissionsId.Values
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId)
            .Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var result =  this._bySubmissionsId.Values
            .Where(x => x.UserId == userId &&
                                 x.Points == points &&
                                 x.ContestId == contestId)
            .Distinct()
            .ToArray();

        if (result.Length == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this._bySubmissionsId.Values
            .Where(x => x.Type == submissionType)
            .Select(x => x.ContestId)
            .Distinct();
    }
}
