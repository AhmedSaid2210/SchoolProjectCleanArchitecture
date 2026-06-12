using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Infrustructure.InfrustructureBases
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly DbSet<UserRefreshToken> _UserRefreshToken;
        public RefreshTokenRepository(AppDbContext dbContext) : base(dbContext)
        {
            _UserRefreshToken = dbContext.Set<UserRefreshToken>();
        }


    }
}
