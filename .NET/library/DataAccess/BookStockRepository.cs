﻿using Microsoft.EntityFrameworkCore;
using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess
{
    public class BookStockRepository : IBookStockRepository
    {
        public BookStockRepository()
        {
        }

        public List<BookStock> GetBookStocks()
        {
            using (var context = new LibraryContext())
            {
                var list = context.Catalogue.Include(x => x.Book).ThenInclude(x => x.Author).Include(x => x.OnLoanTo)
                    .ToList();
                return list;
            }
        }

        public void ReturnBook(BookStock bookStock)
        {
            using (var context = new LibraryContext())
            {
                context.Catalogue.Update(bookStock);
                context.SaveChanges();
            }
        }

    }
}
