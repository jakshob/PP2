using System;
using System.Linq;
using Xunit;

namespace DomainModel.Tests
{
    public class DataServiceTests
    {
        /* Posts */

        [Fact]
        public void Post_Object_HasIdNameAndDescription()
        {
            var p = new Question();
            Assert.Equal(0, p.Id);
            Assert.Equal(0, p.Posttype);
        }

        [Fact]
        public void AnswerToPost19_Has21Results()
        {
            var service = new DataService();
            var answers = service.GetAnswersToQuestion(19,0,21); 
            Assert.Equal(21, answers.Count);
            
        }

        [Fact]
        public void GetAllPosts_CheckCount_andFirstName()
        {
            var service = new DataService();
            var questions = service.GetQuestions(0,2237);
            Assert.Equal(2237, questions.Count);
            Assert.Equal("What is the fastest way to get the value of π?", questions.First().Title);
        }

        [Fact]
        public void GetOnePost_CheckReturn_OnlyOnePost()
        {
            var service = new DataService();
            var questions = service.GetQuestion(19);
            Assert.Equal(19, questions.Id);
            Assert.Equal("What is the fastest way to get the value of π?", questions.Title);
		}

		[Fact]
		public void GetOneComment_CheckReturn_OnlyOneComment() {
			var service = new DataService();
			var comments = service.GetComment(120);
			Assert.Equal(120, comments.Id);
			Assert.Equal(1820, comments.QA_UserId);
		}
		
		[Fact]
		public void GetAllComments_CheckCount_andFirstAuthorid() {
			var service = new DataService();
			var comments = service.GetCommentsByPostId(22106846,0,10);
			Assert.Equal(10, comments.Count);
			Assert.Equal(33534974, comments.First().Id);
		}

        [Fact]
        public void GetAllQuestionsByFavorite()
        {
            var service = new DataService();
            var questionsList = service.GetFavorites("Mogens",0,4);
            Assert.Equal(4, questionsList.Count);
            Assert.Equal(19, questionsList.First().Id);
        }

		[Fact]
		public void GetSearchResults() {
			var service = new DataService();
			var resultList = service.TraverseSearchResults("What is the fastest way", "Mogens", 0, 5);
			Assert.Equal(19, resultList.First().Id);
		}
        
	}
}
