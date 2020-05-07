﻿using Microsoft.EntityFrameworkCore;
using PersonalBudget.Exceptions;
using PersonalBudget.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Business.v1.Objects
{
    public class TransactionBO : ITransactionBO
    {
        private PersonalBudgetContext _context;
        private PersonalBudgetRplContext _contextRpl;

        public TransactionBO(PersonalBudgetContext context, PersonalBudgetRplContext contextRpl)
        {
            _context = context;
            _contextRpl = contextRpl;
        }

        public async Task<Transaction> GetById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Id null");
                }
                Transaction transaction = null;
                if (!ContextRplExists())
                {
                    transaction = await _context.Transaction.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
                }
                else
                {
                    transaction = await _contextRpl.Transaction.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
                }
                if (transaction == null)
                {
                    throw new NotFoundException("Transaction not found");
                }
                return transaction;
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

        public async Task<IEnumerable<Transaction>> GetByUserId(string userId, int limit = 50)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("UserId null");
                }
                IEnumerable<Transaction> transactions = null;
                if (!ContextRplExists())
                {
                    transactions = await _context.Transaction
                                        .Where(t => t.UserId.Equals(userId))
                                        .OrderByDescending(t => t.DtTransaction)
                                        .Take(limit)
                                        .ToListAsync();
                }
                else
                {
                    transactions = await _contextRpl.Transaction
                                        .Where(t => t.UserId.Equals(userId))
                                        .OrderByDescending(t => t.DtTransaction)
                                        .Take(limit)
                                        .ToListAsync();
                }
                return transactions;
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

        public async Task<int> Save(Transaction transaction)
        {
            try
            {
                ValidateFieldsEmpty(transaction);
                transaction.Id = System.Guid.NewGuid().ToString();
                _context.Transaction.Add(transaction);
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

        public async Task<int> Update(string id, Transaction transaction)
        {
            try
            {
                if (id != transaction.Id)
                {
                    throw new Exception("Different id");
                }

                ValidateFieldsEmpty(transaction);

                Transaction transactionUpdate = await GetById(id);
                if (transactionUpdate == null)
                {
                    throw new NotFoundException("Not Found");
                }
                _context.Entry(transactionUpdate).State = EntityState.Detached;

                _context.Entry(transaction).State = EntityState.Modified;
                int result = await _context.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateConcurrencyException e) when (!TransactionExists(id))
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
                var transaction = await GetById(id);
                if (transaction == null)
                {
                    throw new NotFoundException("Transaction not found");
                }
                _context.Transaction.Remove(transaction);
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

        public bool TransactionExists(string id) =>
            ContextRplExists()
            ? _contextRpl.Transaction.AsNoTracking().Any(t => t.Id == id)
            : _context.Transaction.AsNoTracking().Any(t => t.Id == id);

        private bool ContextRplExists() => _contextRpl != null;

        private void ValidateFieldsEmpty(Transaction transaction)
        {
            string result = null;
            if (transaction.MonthRef == 0)
            {
                result = "MonthRef,";
            }
            if (transaction.YearRef == 0)
            {
                result = $"{result} YearRef,";
            }
            if (transaction.DtTransaction == null)
            {
                result = $"{result} Datetime,";
            }
            if (transaction.Amount <= 0)
            {
                result = $"{result} Amount,";
            }
            if (string.IsNullOrEmpty(transaction.UserId))
            {
                result = $"{result} UserId,";
            }
            if (string.IsNullOrEmpty(transaction.CategorieId))
            {
                result = $"{result} CategorieId,";
            }
            if (string.IsNullOrEmpty(transaction.TypeId))
            {
                result = $"{result} TypeId,";
            }
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Remove(result.Length - 1);
                throw new Exception($"{result} is empty");
            }
        }
    }
}
