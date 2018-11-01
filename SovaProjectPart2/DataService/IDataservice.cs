using System;
using System.Collections.Generic;
using System.Text;


namespace DomainModel
{
    public interface IDataService
    {
        // Comments
        List<Comment> GetCommentsByPostId(int id, int page, int pageSize);
        Comment GetComment(int id);
        //Questions
        Question GetQuestion(int id);
        List<Question> GetQuestions(int page, int pageSize);
        List<Question> GetSearchQuestionsSortedByScore(string searchText, int page, int pageSize);
        //Answers
        List<Answer> GetAnswersToQuestion(int postId, int page, int pageSize);
        //History
        List<History> GetHistory(string username, int page, int pageSize);
        //Search
        List<string> GetMostUsedSearchTexts(int page, int pageSize);
        //Favorites
        List<Question> GetFavorites(string username, int page, int pageSize);
        Favorite CreateFavoriteQuestion(int questionId, string username);
        Favorite CreateFavoriteQuestion(int questionId, string username, string note);
        int GetNumberOfQuestions();
        int GetNumberOfComments();

        SOVA_User GetUser(string username);
        SOVA_User CreateUser(string name, string username, string password, string salt);
        

        //List<string> GetPostsWithSameTags(string tags);

    }
}