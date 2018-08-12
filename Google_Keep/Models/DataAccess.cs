using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Google_Keep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep.Models
{
    public class DataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("Note");
        }

        public IEnumerable<Note> GetNotes()
        {
            return _db.GetCollection<Note>("Note").FindAll();
        }


        public Note GetNote(ObjectId id)
        {
            var res = Query<Note>.EQ(p => p.id, id);
            return _db.GetCollection<Note>("Note").FindOne(res);
        }

        public Note Create(Note p)
        {
            _db.GetCollection<Note>("Note").Save(p);
            return p;
        }

        public void Update(ObjectId id, Note p)
        {
            p.id = id;
            var res = Query<Note>.EQ(pd => pd.id, id);
            var operation = Update<Note>.Replace(p);
            _db.GetCollection<Note>("Note").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Note>.EQ(e => e.id, id);
            var operation = _db.GetCollection<Note>("Note").Remove(res);
        }
    }
}