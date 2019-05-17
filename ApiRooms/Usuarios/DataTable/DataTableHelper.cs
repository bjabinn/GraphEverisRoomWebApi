using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace ApiRooms.Usuarios.DataTable
{
    public static class DataTableHelper
    {
        #region Constants
        #endregion

        #region Mapper
        public static IQueryable<TSource> ApplyColumnSettings<TSource>(this IQueryable<TSource> query, DataTableRequest request,
                                                            Func<string, string> columnPropertiePathSelector = null)
        {
            if ((request == null) || ((request.Columns != null) && request.Columns.All(x => !x.SortDirection.HasValue)) || (columnPropertiePathSelector == null))
            {
                return query;
            }
            return query;
        }


        public static IEnumerable<T> ApplyColumnSettings<T>(this IEnumerable<T> list, DataTableRequest request,
                                                      Func<string, string> columnPropertiePathSelector = null)
        {
            if ((request == null) || (request.Columns == null))
            {
                return list;
            }
            return list;
        }

        #endregion
    }

}