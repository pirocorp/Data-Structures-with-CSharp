using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private readonly  Dictionary<int, Competitor> _competitors;
    private readonly Dictionary<int, Competition> _competitions;

    public Olympics()
    {
        this._competitors = new Dictionary<int, Competitor>();
        this._competitions = new Dictionary<int, Competition>();
    }

    public int CompetitorsCount()
    {
        return this._competitors.Count;
    }

    public int CompetitionsCount()
    {
        return this._competitions.Count;
    }

    public void AddCompetitor(int id, string name)
    {
        if (this._competitors.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var competitor = new Competitor(id, name);
        this._competitors.Add(id, competitor);
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this._competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var competition = new Competition(name, id, participantsLimit);
        this._competitions.Add(id, competition);
    }

    public Competition GetCompetition(int id)
    {
        if (!this._competitions.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this._competitions[id];
    }

    public void Compete(int competitorId, int competitionId)
    {
        this.VerifyCompetitorAndCompetitionExists(competitorId, competitionId);

        this._competitions[competitionId]
            .AddCompetitor(this._competitors[competitorId]);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        this.VerifyCompetitorAndCompetitionExists(competitorId, competitionId);
        
        var isSuccess = this._competitions[competitionId].Disqualify(this._competitors[competitorId]);

        if (!isSuccess)
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var result = this._competitors.Values
            .Where(x => x.Name == name)
            .OrderBy(x => x.Id);

        if (!result.Any())
        {
            throw new ArgumentException();
        }

        return result;
    }
    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        return this._competitors.Values
            .Where(x => x.TotalScore > min && x.TotalScore <= max)
            .OrderBy(x => x.Id);
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        return this._competitors.Values
            .Where(x => x.Name.Length >= min && x.Name.Length <= max)
            .OrderBy(x => x.Id);
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this._competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        return this._competitions[competitionId].Competitors.Contains(comp);
    }

    private void VerifyCompetitorAndCompetitionExists(int competitorId, int competitionId)
    {
        if (!this._competitors.ContainsKey(competitorId) ||
            !this._competitions.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }
    }
}