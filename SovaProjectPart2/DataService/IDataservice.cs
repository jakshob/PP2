using System;
using System.Collections.Generic;


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
        List<Question> SearchSova(string sinput, string userName, int page, int pageSize);
        List<Question> TraverseSearchResults(string sinput, string userName, int page, int pageSize);

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

        //Numbers
        int GetNumberOfQuestions();
        int GetNumberOfComments(int inputId);
        int GetNumberOfAnswers(int inputId);

        //Checks
        bool CheckIfUsernameExist(string username);
        int doesPasswordMatch(string username, string password);

        //delete
        void deleteUser(string username, string password);

        //edit user
        void EditUserPassword(string username, string password, string newpassword);

        SOVA_User GetUser(string username);
        List<Object> GetUserPage(string username);

        // SALT FJERNET MIDLERTIDIGT
        SOVA_User CreateUser(string username, string password);


        //List<string> GetPostsWithSameTags(string tags);

    }
}