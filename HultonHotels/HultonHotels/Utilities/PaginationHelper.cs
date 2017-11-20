using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HultonHotels.Utilities
{
    public static class PaginationHelper
    {
        public const int Ellipse = -1;
        public const int Next = -2;
        public const int Previous = -3;


        public static List<int> CreatePaginationWithEllipsis(int currentPage, int totalPages, int breakPoint)
        {
            var pagination = new List<int>();
            if (currentPage != 0)
            {
                pagination.Add(Previous);
            }


            if (totalPages < 9)
            {
                for (var i = 0; i < totalPages + 1; i++)
                {
                    pagination.Add(i);
                }
            }
            else
            {
                if (currentPage < breakPoint)
                {
                    for (var i = 0; i <= breakPoint; i++)
                    {
                        pagination.Add(i);
                    }
                    pagination.Add(Ellipse);
                    pagination.Add(totalPages);
                }
                else if (currentPage <= totalPages - breakPoint && currentPage >= breakPoint - 4)
                {
                    pagination.Add(0);
                    pagination.Add(-1);

                    for (var i = currentPage - 2; i <= currentPage + 2; i++)
                    {
                        pagination.Add(i);
                    }

                    pagination.Add(-1);
                    pagination.Add(totalPages);

                }
                else
                {
                    pagination.Add(0);
                    pagination.Add(-1);


                    for (var i = totalPages - breakPoint; i <= totalPages; i++)
                    {
                        pagination.Add(i);
                    }
                }
            }

            if (currentPage != totalPages)
            {
                pagination.Add(Next);
            }


            return pagination;
        }
    }
}