using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    interface IDataService
    {
        List<Comment> GetComments(int postId, int page, int pageSize);
        Comment GetComment(int id);
        List<Question> GetQuestions(int page, int pageSize);
        List<Question> GetSearchQuestionsSortedByScore(string searchText, int page, int pageSize);
        List<Answer> GetAnswerToQuestion(int postId, int page, int pageSize);
        Question GetQuestion(int id);
        List<Question> GetFavorites(string username, int page, int pageSize);
        List<string> GetHistory(string username, int page, int pageSize);
        //List<string> GetPostsWithSameTags(string tags);
        List<string> GetMostUsedSearchTexts(int page, int pageSize);
        Favorite CreateFavoriteQuestion(int questionId, string username);
        Favorite CreateFavoriteQuestion(int questionId, string username, string note);
    }
}
