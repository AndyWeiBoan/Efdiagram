using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Efdiagram.Extensions {
    public static class ActivatorExtensions {

        public static DbContext CrteateDbContext(Type type) {
            if (type.GetConstructor(Type.EmptyTypes) != default) {
                return (DbContext)Activator.CreateInstance(type);
            }
            var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
            var optionType = typeof(DbContextOptions<>).MakeGenericType(type);
            var constructor = constructors.FirstOrDefault(c =>
                c.GetParameters().Length == 1 && c.GetParameters().Any(p => p.ParameterType == optionType));

            if (constructor == default) {
                throw new NotSupportedException($"No valid constructor of dbcontext class.({Type.FilterName})");
            }
            var builderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(type);
            var builder = (DbContextOptionsBuilder)Activator.CreateInstance(builderType);
            builder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test");
            return (DbContext)Activator.CreateInstance(type, builder.Options);
        }
    }
}
