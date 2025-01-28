﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SharedLibary.DTOs;

namespace AuthServer.Core.Services
{
    //Entityi Dto'ya çevirip döndüren metotlar burada olacak
    public interface IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        Task<Response<TDto>> GetByIdAsync(int id);
        Task<Response<IEnumerable<TDto>>> GetAllAsync();
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);
        Task<Response<TDto>> AddAsync(TDto dto);
        Task<Response<NoContentDto>> Remove(int id);
        Task<Response<NoContentDto>> Update(TDto dto,int id);
    }
}
