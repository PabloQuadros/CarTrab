using CarTrab.DataBase.Mongo;
using CarTrab.Entities;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarTrab.Services
{
    public class MarcaService
    {
        public readonly IMongoCollection<Marca> _marcaCollection;

        public MarcaService(IOptions<MongoSettings> mongoSettingsPar)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _marcaCollection = mongoSettings._iMongoDatabase.GetCollection<Marca>("Marca");
        }

        public async Task<bool> Create(Marca newMarca)
        {
            try
            {
                Marca marca = newMarca;
                marca.id = ObjectId.GenerateNewId().ToString();
                await _marcaCollection.InsertOneAsync(marca);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Marca>> GetAll()
        {
            try
            {
                IEnumerable<Marca> marcaList = await _marcaCollection.Find(_ => true).ToListAsync();
                return marcaList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Marca> GetById(string id)
        {
            try
            {
                Marca marca = await _marcaCollection.Find(c => c.id == id).FirstOrDefaultAsync();
                if (marca == null)
                {
                    throw new Exception("O Marca não foi localizada.");
                }
                return marca;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> Delete(string id)
        {
            try
            {
                Marca marca = await _marcaCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                if (marca == null)
                {
                    throw new Exception("Marca não localizado.");
                }
                await _marcaCollection.DeleteOneAsync(x => x.id == id);
                return true; ;
            }
            catch (Exception ex)
            {
                return false; ;
            }
        }

        public async Task<bool> Update(Marca newMarca)
        {
            try
            {
                Marca marca = await _marcaCollection.Find(x => x.id == newMarca.id).FirstOrDefaultAsync();
                if (marca == null)
                {
                    throw new Exception("Marca não localizado.");
                }
                await _marcaCollection.ReplaceOneAsync(x => x.id == marca.id, newMarca);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
