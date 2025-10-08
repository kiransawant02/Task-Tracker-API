using System.Data;

namespace Task_Tracker_API.BLL.Utility
{
    public static class Utils
    {
        public static bool IsValidDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return false;

            foreach (DataRow row in dt.Rows)
            {
                bool hasValidValue = false;

                foreach (var item in row.ItemArray)
                {
                    if (item != null && item != DBNull.Value && !string.IsNullOrWhiteSpace(item.ToString()))
                    {
                        hasValidValue = true;
                        break; // Exit inner loop early since the row is valid
                    }
                }

                if (!hasValidValue)
                    return false; // Entire row is empty or invalid
            }

            return true; // All rows are valid
        }

        public static bool IsValidDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables == null || ds.Tables.Count == 0)
                return false;

            foreach (DataTable table in ds.Tables)
            {
                if (IsValidDataTable(table))
                    return true;
            }

            return false;
        }
     
        public static int ConvertToInt(object value, int defaultValue = 0) =>
            (value == null || value == DBNull.Value) ? defaultValue :
            int.TryParse(value.ToString(), out int result) ? result : defaultValue;

        public static decimal ConvertToDecimal(object value, decimal defaultValue = 0) =>
            (value == null || value == DBNull.Value) ? defaultValue :
            decimal.TryParse(value.ToString(), out decimal result) ? result : defaultValue;

        public static bool ToBooConvertl(object value, bool defaultValue = false) =>
            (value == null || value == DBNull.Value) ? defaultValue :
            bool.TryParse(value.ToString(), out bool result) ? result :
            value.ToString().Trim().ToLower() switch
            {
                "1" => true,
                "0" => false,
                "yes" => true,
                "no" => false,
                "true" => true,
                "false" => false,
                _ => defaultValue
            };
    }
}
