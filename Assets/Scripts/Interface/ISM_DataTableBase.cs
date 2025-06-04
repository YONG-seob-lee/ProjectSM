using System.Collections.Generic;

namespace Interface
{
    public interface ISM_DataTableBase {}
    
    public interface ISM_DataTable : ISM_DataTableBase
    {
        void RegisterData();
        void Clear();
    }

    public interface ISM_DataBase { }

    public class SM_Data : ISM_DataBase
    {
        int Key { get; set; }
    }
}