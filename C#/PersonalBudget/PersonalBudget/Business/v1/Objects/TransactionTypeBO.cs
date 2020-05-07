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
    public class TransactionTypeBO : ITransactionTypeBO
    {
        private PersonalBudgetContext _context;
        private PersonalBudgetRplContext _contextRpl;

        public TransactionTypeBO(PersonalBudgetContext context, PersonalBudgetRplContext contextRpl)
        {
            _context = context;
            _contextRpl = contextRpl;
        }

        public async Task<TransactionType> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id is empty!");
                }
                TransactionType transactionType = null;
                if (!ContextRplExists())
                {
                    transactionType = await _context.TransactionType.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
                }
                else
                {
                    transactionType = await _contextRpl.TransactionType.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
                }
                if (transactionType == null)
                {
                    throw new NotFoundException("TransactionType not found");
                }
                return transactionType;
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

        public async Task<IEnumerable<TransactionType>> GetAll()
        {
            try
            {
                IEnumerable<TransactionType> transactionTypes = null;
                if (!ContextRplExists())
                {
                    transactionTypes = await _context.TransactionType.AsNoTracking().ToListAsync();
                }
                else
                {
                    transactionTypes = await _contextRpl.TransactionType.AsNoTracking().ToListAsync();
                }
                if (transactionTypes == null)
                {
                    throw new NotFoundException("TransactionTypes not found!");
                }
                return transactionTypes;
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

        public async Task<int> Save(TransactionType transactionType)
        {
            try
            {
                ValidateFieldsEmpty(transactionType);
                transactionType.Id = System.Guid.NewGuid().ToString();
                _context.TransactionType.Add(transactionType);
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

        public async Task<int> Update(string id, TransactionType transactionType)
        {
            try
            {
                if (id != transactionType.Id)
                {
                    throw new Exception("Different id");
                }

                ValidateFieldsEmpty(transactionType);

                TransactionType typeUpdate = await GetById(id);
                if (typeUpdate == null)
                {
                    throw new NotFoundException("Not Found");
                }
                else
                {
                    _context.Entry(typeUpdate).State = EntityState.Detached;
                }

                _context.Entry(typeUpdate).State = EntityState.Modified;
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException e) when (!TransactionTypeExists(id))
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
                var transactionType = await GetById(id);
                if (transactionType == null)
                {
                    throw new NotFoundException("Transaction Type not found");
                }
                _context.TransactionType.Remove(transactionType);
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

        public bool TransactionTypeExists(string id) =>
            ContextRplExists()
            ? _contextRpl.Categorie.AsNoTracking().Any(c => c.Id == id)
            : _context.Categorie.AsNoTracking().Any(c => c.Id == id);

        private bool ContextRplExists() => _contextRpl != null;

        private void ValidateFieldsEmpty(TransactionType transactionType)
        {
            if (string.IsNullOrEmpty(transactionType.Name))
            {
                throw new Exception("Name is not empty");
            }
        }
    }
}
