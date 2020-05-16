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
    public class CategorieBO : ICategorieBO
    {
        private PersonalBudgetContext _context;
        private PersonalBudgetRplContext _contextRpl;

        public CategorieBO(PersonalBudgetContext context, PersonalBudgetRplContext contextRpl=null)
        {
            _context = context;
            _contextRpl = contextRpl;
        }

        public async Task<Categorie> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id null");
                }
                Categorie categorie = null;
                if (!ContextRplExists())
                {
                    categorie = await _context.Categorie.AsNoTracking().Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
                }
                else
                {
                    categorie = await _contextRpl.Categorie.AsNoTracking().Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
                }
                if (categorie == null)
                {
                    throw new NotFoundException("Categorie not found");
                }
                return categorie;
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

        public async Task<IEnumerable<Categorie>> GetByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("UserId null");
                }
                IEnumerable<Categorie> categories = null;
                if (!ContextRplExists())
                {
                    categories = await _context.Categorie
                                        .AsNoTracking()
                                        .Where(c => c.UserId.Equals(userId))
                                        .ToListAsync();
                }
                else
                {
                    categories = await _contextRpl.Categorie
                                        .AsNoTracking()
                                        .Where(c => c.UserId.Equals(userId))
                                        .ToListAsync();
                }
                return categories;
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

        public async Task<int> Save(Categorie categorie)
        {
            try
            {
                ValidateFieldsEmpty(categorie);
                categorie.Id = System.Guid.NewGuid().ToString();
                _context.Categorie.Add(categorie);
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

        public async Task<int> Update(string id, Categorie categorieUpdate)
        {
            try
            {
                if (id != categorieUpdate.Id)
                {
                    throw new Exception("Different id");
                }
                var categorie = await GetById(id);
                if (categorie == null)
                {
                    throw new NotFoundException();
                }

                ValidateFieldsEmpty(categorieUpdate);

                _context.Entry<Categorie>(categorieUpdate).State = EntityState.Modified;
                int result = await _context.SaveChangesAsync();
                _context.Entry<Categorie>(categorieUpdate).State = EntityState.Detached;
                return result;
            }
            catch (DbUpdateConcurrencyException e) when (!CategorieExists(id))
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Delete(string id)
        {
            try
            {
                var categorie = await GetById(id);
                if (categorie == null)
                {
                    throw new NotFoundException("Categorie not found");
                }
                _context.Entry<Categorie>(categorie).State = EntityState.Detached;
                _context.Categorie.Remove(categorie);
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

        public bool CategorieExists(string id) =>
            ContextRplExists() 
            ? _contextRpl.Categorie.AsNoTracking().Any(c => c.Id == id) 
            : _context.Categorie.AsNoTracking().Any(c => c.Id == id);

        private bool ContextRplExists() => _contextRpl != null;

        private void ValidateFieldsEmpty(Categorie categorie)
        {
            string result = null;
            if (string.IsNullOrEmpty(categorie.Name))
            {
                result = "Name,";
            }
            if (string.IsNullOrEmpty(categorie.UserId))
            {
                result = $"{result} UserId ";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Remove(result.Length - 1);
                throw new Exception($"{result} is empty");
            }
        }
    }
}

