﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Calculator.Aggregations;
using Calculator.Arithmetic;
using LoggingCalculator.AbstractionsAndModels;
using LoggingCalculator.AbstractionsAndModels.Aggregations;
using LoggingCalculator.AbstractionsAndModels.Arithmetic;
using LoggingCalculator.AbstractionsAndModels.Models;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

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

            builder.RegisterType<Calculator<CalculatorValue>>().As<ICalculator<CalculatorValue>>();
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
