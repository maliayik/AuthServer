﻿using SharedLibary.DTOs;
using System.Linq.Expressions;

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

        Task<Response<NoContentDto>> Update(TDto dto, int id);
    }
}