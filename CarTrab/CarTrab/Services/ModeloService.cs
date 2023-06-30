using CarTrab.DataBase.Mongo;
using CarTrab.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CarTrab.Services
{
    public class ModeloService
    {
        public readonly IMongoCollection<Modelo> _modeloCollection;
        public MarcaService _marcaService { get; set; }

        public ModeloService(IOptions<MongoSettings> mongoSettingsPar, MarcaService marcaService)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _modeloCollection = mongoSettings._iMongoDatabase.GetCollection<Modelo>("Modelo");
            _marcaService = marcaService;
        }

        public async Task<bool> Create(Modelo newModelo)
        {
            try
            {
                var marca = _marcaService.GetById(newModelo.id_marca);
                if(marca == null)
                {
                    throw new Exception("Marca não localizada");
                }
                Modelo modelo = newModelo;
                modelo.id = ObjectId.GenerateNewId().ToString();
                await _modeloCollection.InsertOneAsync(modelo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Modelo>> GetAll()
        {
            try
            {
                IEnumerable<Modelo> modeloList = await _modeloCollection.Find(_ => true).ToListAsync();
                return modeloList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Modelo> GetById(string id)
        {
            try
            {
                Modelo modelo = await _modeloCollection.Find(c => c.id == id).FirstOrDefaultAsync();
                if (modelo == null)
                {
                    throw new Exception("O modelo não foi localizada.");
                }
                return modelo;
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
                Modelo modelo = await _modeloCollection.Find(x => x.id == id).FirstOrDefaultAsync();
                if (modelo == null)
                {
                    throw new Exception("Modelo não localizado.");
                }
                await _modeloCollection.DeleteOneAsync(x => x.id == id);
                return true; ;
            }
            catch (Exception ex)
            {
                return false; ;
            }
        }

        public async Task<bool> Update(Modelo newModelo)
        {
            try
            {
                var marca = _marcaService.GetById(newModelo.id_marca);
                if (marca == null)
                {
                    throw new Exception("Marca não localizada");
                }
                Modelo modelo = await _modeloCollection.Find(x => x.id == newModelo.id).FirstOrDefaultAsync();
                if (modelo == null)
                {
                    throw new Exception("Modelo não localizado.");
                }
                await _modeloCollection.ReplaceOneAsync(x => x.id == modelo.id, newModelo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
