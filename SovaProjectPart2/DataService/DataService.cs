using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomainModel
{
    public class DataService : IDataService
    {
       
        readonly List<SOVA_User> _users = new List<SOVA_User>();

        public DataService()
        {
            _users.Add(new SOVA_User()
            {
                Username = "ObiWan.Kenobi"
            });
        }

        public SOVA_User GetUser(string username)
        {
            return _users.FirstOrDefault(x => x.Username == username);
        }

        
        public SOVA_User CreateUser(string username, string password)
        {
            
            var user = new SOVA_User()
            {
                Username = username,
                Password = password,
            };
            _users.Add(user);
            return user;
        }

        public int doesPasswordMatch(string username, string password)
        {
            using (var db = new SovaContext())
            {
                int number = 0;
                bool userMatch = db.SOVA_Users.Any(x => x.Username == username);
                bool PasswordMatch = db.SOVA_Users.Any(x => x.Username == username && x.Password == password);

                //number referer til errorMessages sent ud
                if (userMatch && PasswordMatch) { number = 1; }
                else if (!userMatch) { number = 2; }
                else if (userMatch && !PasswordMatch) { number = 3; }

                return number;
            }
        }

        public void deleteUser(string username, string password)
        {
            using (var db = new SovaContext())
            {

                var user = db.SOVA_Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
                db.SOVA_Users.Remove(user);
                db.SaveChanges();

            }
        }
        public void EditUserPassword(string username, string password, string newpassword)
        {
            using (var db = new SovaContext()) {

                SOVA_User userToChange = db.SOVA_Users.Where(x => x.Username == username).FirstOrDefault();
                userToChange.Password = newpassword;
                db.SOVA_Users.Update(userToChange);
                db.SaveChanges();
            }

        }

        public List<Object> GetUserPage(string username) {

            List<Question> favorites = GetFavorites(username, 0, 0);
            string messageFavorites = "YOUR CHOSEN FAVORITE POSTS:";
            List<History> history = GetHistory(username, 0, 0);
            string messageHistory = "YOUR PERSONAL SEARCH HISTORY:";


            //Objekter kunne tilføjes direkte til liste, men det her giver bedre overblik i svaret fra webserver.
            List<Object> myList = new List<Object>();
            myList.Add(messageFavorites);
            myList.Add(favorites);
            myList.Add(messageHistory);
            myList.Add(history);

            return myList;

        }

        public List<Answer> GetAnswers()
        {
            using (var db = new SovaContext())
            {
                return db.Answers.ToList();
            }
        }


        public List<Question> GetQuestions(int page, int pageSize) {

            using (var db = new SovaContext())
            {
                return db.Questions
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public List<Answer> GetAnswersToQuestion(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var answerList = db.Answers.Where(x => x.ParentId == inputId);
                // Skal vi lave sort by score???

                return answerList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
        public int GetNumberOfAnswers(int inputId)
        {
            using (var db = new SovaContext())
            {
                var answers = db.Answers.Where(x => x.ParentId == inputId);
                var numbers = answers.Count();
                return numbers;
            }
            
        }


        public Answer GetAnswer(int inputId)
        {

            using (var db = new SovaContext())
            {
                return db.Answers.Find(inputId);
            }
		}
		public Question GetQuestion(int inputId) {

			using (var db = new SovaContext()) {
				return db.Questions.Find(inputId);
			}
		}
		public List<Comment> GetCommentsByPostId(int inputId, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {
                var commentList = db.Comments.Where(x => x.PostId == inputId);

                return commentList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
        public Comment GetComment(int id)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Find(id);
            }
        }

        public List<RelevantWord> GetRelevantWords(string word, int page, int pageSize)
        {
            using (var db = new SovaContext())

            {
                List<RelevantWord> tempList = new List<RelevantWord>();


                foreach (var result in db.RelevantWords.FromSql("select * from \"wordToWords\"({0},{1})", word,
                    "Mogens"))
                {
                    if (result.word != word)
                    {
                        var tmp = new RelevantWord
                        {
                            word = result.word,
                            word_f = result.word_f
                        };
                        tempList.Add(tmp);
                    }

                }

                return tempList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }

        public List<SearchResult> GetSearchQuestionsSortedByScore(string searchText, int page, int pageSize) {

            using (var db = new SovaContext())
            {
                
                List<SearchResult> tempList = new List<SearchResult>();

                foreach (var result in db.SearchResults.FromSql("select * from \"bestMatchSova\"({0},{1})", searchText,
                    "Mogens"))
                {
                    if (result.posttype == 1)
                    {
                        var tmp = new SearchResult
                        {
                            body = result.body,
                            postId = result.postId,
                            rank = result.rank,
                            posttype = result.posttype
  
                           
                        };
                        tempList.Add(tmp);
                    }

                }
                

                return tempList
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }    
        }

        public List<History> GetHistory(string username, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var historyByUser = db.Histories.Where(h => h.SOVA_UserUsername == username);
                return historyByUser
                  /*  .Skip(page * pageSize)
                    .Take(page) */
                    .ToList(); 
 
            }
        }

        public List<string> GetMostUsedSearchTexts(int page, int pageSize)
        {
            using (var db = new SovaContext()){

                var listOfSearchWords = db.Histories.Select(x => x.SearchText).ToList();

                return listOfSearchWords;
                /*.Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();*/
            }
        }

        public List<Question> GetFavorites(string username, int page, int pageSize)
        {
            using (var db = new SovaContext())
            {

                var listOfQuestions = db.Favorites
                    .Where(x => x.SOVA_UserUsername == username)
                    .Join(db.Questions, x => x.PostId, y => y.Id, (x, y) => y);

                return listOfQuestions.ToList()
                .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
        public bool CheckIfUsernameExist(string username) {

            using (var db = new SovaContext()) {

                bool doesUsernameExist = db.SOVA_Users.Any(x => x.Username == username);

                return doesUsernameExist;
            }
            
        }

        public Favorite CreateFavoriteQuestion(int questionId, string username)
        {
            using (var db = new SovaContext())
            {
                Favorite record = new Favorite();
                record.PostId = questionId;
                record.SOVA_UserUsername = username;
                db.Favorites.Add(record);
                db.SaveChanges();

                return record;
            }

        }

        public Favorite CreateFavoriteQuestion(int questionId, string username, string note)
        {
            using (var db = new SovaContext())
            {
                Favorite record = new Favorite();
                record.PostId = questionId;
                record.SOVA_UserUsername = username;
                record.Note = note;
                db.Favorites.Add(record);
                db.SaveChanges();

                return record;
            }

        }

        public int GetNumberOfQuestions()
        {
            using (var db = new SovaContext())
            {
                return db.Questions.Count();
            }
        }


        public int GetNumberOfComments(int inputId)
        {
            using (var db = new SovaContext())
            {
                return db.Comments.Where(x => x.PostId == inputId).Count();
            }
        }

		public List<Question> SearchSova(string sinput, string userName, int page, int pageSize) {
			using (var db = new SovaContext()) {
				var resultList = db.Questions.Where(x => x.Body.Contains(sinput) | x.Title.Contains(sinput));
                var resultListSorted = resultList.OrderByDescending(x => x.Score).ToList();

                History history = new History {
					SOVA_UserUsername = userName,
					CreationDate = DateTime.Now,
					SearchText = sinput
				};
				db.Histories.Add(history);
				db.SaveChanges();

				return resultListSorted
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .ToList();

            }
		}
		public List<Question> TraverseSearchResults(string sinput, string userName, int page, int pageSize) {
			using (var db = new SovaContext()) {
				var resultList = db.Questions.Where(x => x.Body.Contains(sinput) | x.Title.Contains(sinput));
                var resultListSorted = resultList.OrderByDescending(x => x.Score).ToList();

                return resultListSorted
					.Skip(page * pageSize)
					.Take(pageSize)
					.ToList();
			}
		}

	}
}