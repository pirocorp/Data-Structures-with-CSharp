namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private readonly Dictionary<string, Bunny> _bunnyByName;
        private readonly OrderedDictionary<int, LinkedList<Bunny>[]> _bunnyByRoomByTeam;
        private readonly OrderedDictionary<int, OrderedSet<Bunny>> _bunnyByTeam;

        private static readonly IComparer<Bunny> SuffixComparator = new OrdinalSuffixComparator();
        private readonly OrderedSet<Bunny> _bySuffix;

        public BunnyWarsStructure()
        {
            this._bunnyByName = new Dictionary<string, Bunny>();
            this._bunnyByRoomByTeam = new OrderedDictionary<int, LinkedList<Bunny>[]>();
            this._bunnyByTeam = new OrderedDictionary<int, OrderedSet<Bunny>>();
            this._bySuffix = new OrderedSet<Bunny>(SuffixComparator);
        }

        public int BunnyCount => this._bunnyByName.Count;

        public int RoomCount => this._bunnyByRoomByTeam.Count;

        public void AddRoom(int roomId)
        {
            if (this._bunnyByRoomByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            this._bunnyByRoomByTeam[roomId] = new LinkedList<Bunny>[5];

            for (var i = 0; i < 5; i++)
            {
                this._bunnyByRoomByTeam[roomId][i] = new LinkedList<Bunny>();
            }
        }

        public void AddBunny(string name, int team, int roomId)
        {
            TeamValidator(team);

            if (this._bunnyByName.ContainsKey(name) ||
                !this._bunnyByRoomByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            if (!this._bunnyByTeam.ContainsKey(team))
            {
                this._bunnyByTeam[team] = new OrderedSet<Bunny>();
            }

            var bunny = new Bunny(name, team, roomId);
            this._bunnyByName.Add(name, bunny);
            this._bunnyByRoomByTeam[roomId][team].AddLast(bunny);
            this._bunnyByTeam[team].Add(bunny);
            this._bySuffix.Add(bunny);
        }

        public void Remove(int roomId)
        {
            if (!this._bunnyByRoomByTeam.ContainsKey(roomId))
            {
                throw new ArgumentException();
            }

            var bunniesToBeRemoves = this._bunnyByRoomByTeam[roomId];
            this._bunnyByRoomByTeam.Remove(roomId);

            foreach (var team in bunniesToBeRemoves)
            {
                foreach (var bunny in team)
                {
                    this._bunnyByName.Remove(bunny.Name);
                    this._bunnyByTeam[bunny.Team].Remove(bunny);
                    this._bySuffix.Remove(bunny);
                }
            }
        }

        public void Next(string bunnyName)
        {
            this.ValidateBunnyExists(bunnyName);

            if (this._bunnyByRoomByTeam.Count == 1)
            {
                return;
            }

            var bunny = this._bunnyByName[bunnyName];
            this._bunnyByRoomByTeam[bunny.RoomId][bunny.Team].Remove(bunny);

            var currentRoomId = bunny.RoomId;
            
            var nextRoom = this._bunnyByRoomByTeam.RangeFrom(currentRoomId, false).FirstOrDefault();
            
            if (nextRoom.Value == null)
            {
                var firstRoomId = this._bunnyByRoomByTeam.Keys.First();
                bunny.RoomId = firstRoomId;
                this._bunnyByRoomByTeam[firstRoomId][bunny.Team].AddLast(bunny);
                return;
            }

            bunny.RoomId = nextRoom.Key;
            this._bunnyByRoomByTeam[nextRoom.Key][bunny.Team].AddLast(bunny);
        }

        public void Previous(string bunnyName)
        {
            this.ValidateBunnyExists(bunnyName);

            if (this._bunnyByRoomByTeam.Count == 1)
            {
                return;
            }

            var bunny = this._bunnyByName[bunnyName];
            this._bunnyByRoomByTeam[bunny.RoomId][bunny.Team].Remove(bunny);

            var currentRoomId = bunny.RoomId;

            var prevRoom = this._bunnyByRoomByTeam.RangeTo(currentRoomId, false).Reversed().FirstOrDefault();

            if (prevRoom.Value == null)
            {
                var lastRoomId = this._bunnyByRoomByTeam.Keys.Last();
                bunny.RoomId = lastRoomId;
                this._bunnyByRoomByTeam[lastRoomId][bunny.Team].AddLast(bunny);
                return;
            }

            bunny.RoomId = prevRoom.Key;
            this._bunnyByRoomByTeam[prevRoom.Key][bunny.Team].AddLast(bunny);
        }

        public void Detonate(string bunnyName)
        {
            this.ValidateBunnyExists(bunnyName);

            var detonatedBunny = this._bunnyByName[bunnyName];
            var room = this._bunnyByRoomByTeam[detonatedBunny.RoomId];

            for (int i = 0; i < 5; i++)
            {
                if (i == detonatedBunny.Team)
                {
                    continue;
                }

                var team = room[i];
                var current = team.First;

                while (current != null)
                {
                    var bunny = current.Value;

                    if (bunny.Team != detonatedBunny.Team)
                    {
                        bunny.Health -= 30;

                        if (bunny.Health <= 0)
                        {
                            detonatedBunny.Score++;
                            this._bunnyByName.Remove(bunny.Name);
                            this._bunnyByTeam[bunny.Team].Remove(bunny);
                            this._bySuffix.Remove(bunny);
                            var next = current.Next;
                            team.Remove(current);
                            current = next;
                            continue;
                        }
                    }

                    current = current.Next;
                }
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            TeamValidator(team);
            return this._bunnyByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            var low = new Bunny(suffix, 0, 0);
            var high = new Bunny(char.MaxValue + suffix, 0, 0);

            return this._bySuffix.Range(low, true, high, false);
        }

        private static void TeamValidator(int team)
        {
            if (team < 0 || team > 4)

            {
                throw new IndexOutOfRangeException();
            }
        }

        private void ValidateBunnyExists(string bunnyName)
        {
            if (!this._bunnyByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException();
            }
        }
    }
}
