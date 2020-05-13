using Microsoft.EntityFrameworkCore;
using PersonalBudget.Exceptions;
using PersonalBudget.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1.Objects
{
    public class ReleaseBO : IReleaseBO
    {
        private PersonalBudgetContext _context;
        private PersonalBudgetRplContext _contextRpl;

        public ReleaseBO(PersonalBudgetContext context, PersonalBudgetRplContext contextRpl)
        {
            _context = context;
            _contextRpl = contextRpl;
        }

        public async Task<Release> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id null");
                }
                Release release = null;
                if (!ContextRplExists())
                {
                    release = await _context.Release.AsNoTracking().FirstOrDefaultAsync(r => r.Id.Equals(id));
                }
                else
                {
                    release = await _contextRpl.Release.AsNoTracking().FirstOrDefaultAsync(r => r.Id.Equals(id));
                }
                if (release == null)
                {
                    throw new NotFoundException("Release not found");
                }
                return release;
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Release>> GetByTransactionId(string transactionId)
        {
            try
            {
                if (string.IsNullOrEmpty(transactionId))
                {
                    throw new Exception("TransactionId null");
                }
                IEnumerable<Release> releases = null;
                if (!ContextRplExists())
                {
                    releases = await _context.Release
                                        .AsNoTracking()
                                        .Where(r => r.TransactionId.Equals(transactionId))
                                        .ToListAsync();
                }
                else
                {
                    releases = await _contextRpl.Release
                                        .AsNoTracking()
                                        .Where(r => r.TransactionId.Equals(transactionId))
                                        .ToListAsync();
                }
                return releases;
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Release release)
        {
            try
            {
                ValidateFieldsEmpty(release);
                
                release.Id = System.Guid.NewGuid().ToString();
                _context.Release.Add(release);
                int result = await _context.SaveChangesAsync();

                return result;
            }
            catch (DbException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Update(string id, Release release)
        {
            try
            {
                if (id != release.Id)
                {
                    throw new Exception("Different id");
                }

                ValidateFieldsEmpty(release);

                _context.Entry(release).State = EntityState.Modified;
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException e) when (!ReleaseExists(id))
            {
                throw e;
            }
            catch (DbException e)
            {
                throw e;
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Delete(string id)
        {
            try
            {
                var release = await GetById(id);
                if (release == null)
                {
                    throw new NotFoundException("Release not found");
                }
                _context.Entry(release).State = EntityState.Detached;
                _context.Release.Remove(release);
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (NotFoundException e)
            {
                throw e;
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        public bool ReleaseExists(string id) =>
            ContextRplExists()
            ? _contextRpl.Release.AsNoTracking().Any(r => r.Id == id)
            : _context.Release.AsNoTracking().Any(r => r.Id == id);

        private bool ContextRplExists() => _contextRpl != null;

        private void ValidateFieldsEmpty(Release release)
        {
            string result = null;
            if (release.MonthRef == 0)
            {
                result = "MonthRef,";
            }
            if (release.YearRef == 0)
            {
                result = $"{result} YearRef,";
            }
            if (release.Amount <= 0)
            {
                result = $"{result} Amount,";
            }
            if (string.IsNullOrEmpty(release.TransactionId))
            {
                result = $"{result} TransactionId,";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Remove(result.Length - 1);
                throw new Exception($"{result} is empty");
            }
        }
    }
}
