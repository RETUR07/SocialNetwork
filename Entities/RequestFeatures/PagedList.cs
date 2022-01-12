﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Entities.RequestFeatures
{
    public class PagedList<T> : List<T>{
        public MetaData MetaData { get; set; }

        public PagedList() { }//for automapper

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }

        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            var items = source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }

        public static PagedList<T> ToReversedPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            var items = source
            .Skip(count - (pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();

            items.Reverse();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

