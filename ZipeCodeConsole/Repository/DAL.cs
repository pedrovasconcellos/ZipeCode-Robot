using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using System.Linq;

namespace ZipeCodeConsole.Repository
{
    public class DAL
    {
        private readonly static Object objectLockedDal = new Object();

        public bool ConectionIsOK()
        {
            lock (objectLockedDal)
            {
                try
                {
                    using (Context context = new Context())
                    {
                        context.Database.Connection.Open();
                        if (context.Database.Connection.State == ConnectionState.Open)
                        {
                            context.Database.Connection.Close();
                            return true;
                        }
                        else
                        {
                            Thread.Sleep(TimeSpan.FromMinutes(5));
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(String.Concat(ex.Message, Environment.NewLine));
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                    return false;
                }
            }
        }

        public async Task<int> Insert<TEntity>(object obj) where TEntity : class
        {
            return await Task<int>.Run(() =>
            {
                try
                {
                    while (!ConectionIsOK()) { }
                    int affectedItems;
                    using (Context context = new Context())
                    {
                        context.Entry(obj).State = EntityState.Added;
                        affectedItems = context.SaveChanges();
                    }
                    return affectedItems;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public int Update<TEntity>(object obj) where TEntity : class
        {
            try
            {
                while (!ConectionIsOK()) { }
                int affectedItems;
                using (Context context = new Context())
                {
                    context.Entry(obj).State = EntityState.Modified;
                    affectedItems = context.SaveChanges();
                }

                return affectedItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete<TEntity>(object obj) where TEntity : class
        {
            try
            {
                while (!ConectionIsOK()) { }
                int affectedItems;
                using (Context context = new Context())
                {
                    context.Entry(obj).State = EntityState.Deleted;
                    affectedItems = context.SaveChanges();
                }

                return affectedItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ExistZipeCode<TEntity>(object obj) where TEntity : class
        {
            try
            {
                while (!ConectionIsOK()) { }
                using (Context context = new Context())
                {
                    return context.Set<ZipeCode>().Any((x) => x.cep == ((ZipeCode)obj).cep);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int LastZipeCodeInsert()
        {
            try
            {
                while (!ConectionIsOK()) { }
                using (Context context = new Context())
                {
                    return Convert.ToInt32(context.Set<ZipeCode>().OrderByDescending(x => x.cep).FirstOrDefault()?.cep);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CountZipeCode()
        {
            try
            {
                while (!ConectionIsOK()) { }
                using (Context context = new Context())
                {
                    return context.ZipeCode.Count();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
