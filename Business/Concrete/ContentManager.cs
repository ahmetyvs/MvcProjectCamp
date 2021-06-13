﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class ContentManager : IContentService
    {
        private IContentDal _contentDal;

        public ContentManager(IContentDal contentDal)
        {
            _contentDal = contentDal;
        }

        public List<Content> GetAll()
        {
            return _contentDal.List();
        }

        public List<Content> GetAllByWriter()
        {
            return _contentDal.List(x => x.WriterId == 1);
        }

        public void Add(Content content)
        {
            _contentDal.Add(content);
        }

        public void Update(Content content)
        {
            _contentDal.Update(content);
        }

        public void Delete(Content content)
        {
            _contentDal.Delete(content);
        }

        public List<Content> GetListById(int id)
        {
            return _contentDal.List(x => x.HeadingId == id);
        }

    }
}
