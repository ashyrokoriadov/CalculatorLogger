using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.AttributeFilters;
using Calculator.Aggregations;
using Calculator.Arithmetic;
using Calculator.Validator;
using LoggingCalculator.AbstractionsAndModels;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;
using LoggingCalculator.AbstractionsAndModels.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Calculator.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddControllersAsServices();

            var builder = new ContainerBuilder();
            builder.Populate(services);

            builder.RegisterType<AverageAggregator>().As<IAverageAggregator<CalculatorValue>>();
            builder.RegisterType<MinAggregator>().As<IMinAggregator<CalculatorValue>>();
            builder.RegisterType<MaxAggregator>().As<IMaxAggregator<CalculatorValue>>();
            builder.RegisterType<AggregationCalculator<CalculatorValue>>().As<IAggregationCalculator<CalculatorValue>>();

            builder.RegisterType<Adder>().As<IAdder<CalculatorValue>>();
            builder.RegisterType<Subtractor>().As<ISubtractor<CalculatorValue>>();
            builder.RegisterType<Divider>().As<IDivider<CalculatorValue>>();
            builder.RegisterType<Multiplier>().As<IMultiplier<CalculatorValue>>();
            builder.RegisterType<ArithmeticCalculator<CalculatorValue>>().As<IArithmeticCalculator<CalculatorValue>>();

            builder.RegisterType<IsNullValidator>().Named<IValidator<CalculatorValue>>("NullValidator");
            builder.RegisterType<IsZeroValidator>().Named<IValidator<CalculatorValue>>("ZeroValidator");
            var controllers = typeof(Startup).Assembly.GetTypes().Where(t => t.BaseType == typeof(ControllerBase)).ToArray();
            builder.RegisterTypes(controllers).WithAttributeFiltering();

            builder.RegisterType<Calculator<CalculatorValue>>().As<ICalculator<CalculatorValue>>().SingleInstance();
            AutofacContainer = builder.Build();

            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
