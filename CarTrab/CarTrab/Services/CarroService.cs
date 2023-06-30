using CarTrab.DataBase.Mongo;
using CarTrab.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarTrab.Services
{
    public class CarroService
    {
        public readonly IMongoCollection<Carro> _carroCollection;
        public ModeloService _modeloService { get; set; }

        public CarroService(IOptions<MongoSettings> mongoSettingsPar, ModeloService modeloService)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _carroCollection = mongoSettings._iMongoDatabase.GetCollection<Carro>("Carro");
            _modeloService = modeloService;
        }

        public async Task<bool> Create(Carro newCarro)
        {
            try
            {
                var modelo = _modeloService.GetById(newCarro.id_modelo);
                if (modelo == null)
                {
                    throw new Exception("Modelo não localizada");
                }
                Carro carro = newCarro;
                carro.id = ObjectId.GenerateNewId().ToString();
                await _carroCollection.InsertOneAsync(carro);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Carro>> GetAll()
        {
            try
            {
                IEnumerable<Carro> carroList = await _carroCollection.Find(_ => true).ToListAsync();
                return carroList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Carro> GetById(string id)
        {
            try
            {
                Carro carro = await _carroCollection.Find(c => c.id == id).FirstOrDefaultAsync();
                if (carro == null)
                {
                    throw new Exception("O Carro não foi localizada.");
                }
                return carro;
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
                Carro carro = await _carroCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                if (carro == null)
                {
                    throw new Exception("Carro não localizado.");
                }
                await _carroCollection.DeleteOneAsync(x => x.id == id);
                return true; ;
            }
            catch (Exception ex)
            {
                return false; ;
            }
        }

        public async Task<bool> Update(Carro newCarro)
        {
            try
            {
                var modelo = _modeloService.GetById(newCarro.id_modelo);
                if (modelo == null)
                {
                    throw new Exception("modelo não localizada");
                }
                Carro carro = await _carroCollection.Find(x => x.id == newCarro.id).FirstOrDefaultAsync();
                if (carro == null)
                {
                    throw new Exception("Carro não localizado.");
                }
                await _carroCollection.ReplaceOneAsync(x => x.id == carro.id, newCarro);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

