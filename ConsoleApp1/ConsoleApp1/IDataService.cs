using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    interface IDataService
    {
        List<Comment> GetComments(int page, int pageSize);
        Comment GetComment(int id);
        List<Question> GetQuestions(int page, int pageSize);
        List<Question> GetQuestionsBySearchSortedByScore(string searchText, int page, int pageSize);
        List<Answer> GetAnswerToQuestion(int postId, int page, int pageSize);
        Question GetQuestion(int id);
        List<Question> GetFavorites(string sova_UserUsername, int page, int pageSize);
        List<string> GetHistory(string sova_UserUsername, int page, int pageSize);
        List<string> GetRelatedKeywords(string searchText);
        List<string> GetMostUsedSearchTexts(int page, int pageSize);
        Favorite CreateFavoriteQuestion(int questionId, string sova_UserUsername);
        Favorite CreateFavoriteQuestion(int questionId, string sova_UserUsername, string note);
    }
}
