using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Webservice.Models;

namespace Webservice
{
    public class Startup
    {
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<IDataService,DataService>();
        }

       public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            MapperConfig();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();
            app.UseMvc();

      
        }

        private void MapperConfig()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Answer, AnswerListModel>();
                cfg.CreateMap<Comment, CommentListModel>();
                cfg.CreateMap<Favorite, FavoriteListModel>();
                cfg.CreateMap<Favorite, FavoriteModel>();
                cfg.CreateMap<History, HistoryListModel>();           
                cfg.CreateMap<Question, QuestionListModel>();
                cfg.CreateMap<Question, QuestionModel>();
                cfg.CreateMap<SearchResult, SearchListModel>();
                cfg.CreateMap<RelevantWord, WordListModel>();

            });
        }

    }
}
