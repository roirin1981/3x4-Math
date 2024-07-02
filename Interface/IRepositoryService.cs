using _3x4_Math.ModelDB;
using _3x4_Math.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3x4_Math.Interface;

public interface IRepositoryService
{
    public Task<bool> DeleteDataBase();
    public Task<ResultUpdateDB> LoadService_SaveDB();
    public Task<bool> AddItemDB(ItemDB o);
    public Task<List<ItemDB>> GetAllItems();
    public Task<bool> ClearScores();
    public Task<bool> ClearItemsPesos();

    public Task<bool> AddItemPesoDB(int Num1, int Num2, bool val);

    public Task<List<ItemPesosMultiDB>> GetAllPesosItems();
    public Task<ItemPesosMultiDB> GetItemPesosWorst();
}
