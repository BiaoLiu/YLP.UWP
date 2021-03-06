﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Https
{
    /// <summary>
    /// 接口请求URL地址
    /// </summary>
    public class ServiceURL
    {
        private const string Host = "http://testapi.yuleband.com/";
        //private const string Host = "http://localhost:24369/";

        //private const string Host = "http://api.yuleband.com/";

        #region 首页模块

        public const string Main_SlideArticleList = Host + "api/main-gettoparticle";

        public const string Main_R2ArticleList = Host + "api/main-getr2articlelist";

        public const string Main_R3ArticleList = Host + "api/main-getr3articlelist";

        public const string Main_UArticleList = Host + "api/main-gettasklist";

        #endregion


        #region 会员模块

        /// <summary>
        /// 会员注册
        /// </summary>
        public const string Member_Register = Host + "api/member-register";

        /// <summary>
        /// 会员登录
        /// </summary>
        public const string Member_Login = Host + "api/member-login";

        /// <summary>
        /// 更新会员信息
        /// </summary>
        public const string Member_UpdateInfo = Host + "api/member-updateuserinfo";

        /// <summary>
        /// 更新会员头像
        /// </summary>
        public const string Member_UpdateAvatar = Host + "api/member-updateavatar";

        #endregion

        #region 用户作品模块

        /// <summary>
        /// 获取用户作品列表
        /// </summary>
        public const string UArticle_UArticleList = Host + "api/task-gettasklist";

        /// <summary>
        /// 新增用户作品
        /// </summary>
        public const string UArticle_CreateUArticle = Host + "api/task-createtask";

        #endregion

        /// <summary>
        /// 创建用户行为
        /// </summary>
        public const string Common_CreateUserAction = Host + "api/common-createuseraction";


        /// <summary>
        /// 获取评论列表
        /// </summary>
        public const string Comment_CommentList = Host + "api/comment-getcommentlist";

        /// <summary>
        /// 新增评论
        /// </summary>
        public const string Comment_CreateComment = Host + "api/comment-createcomment";


        #region 专题模块

        /// <summary>
        /// 获取文章详情
        /// </summary>
        public const string Article_GetArticleInfo = Host + "api/article-getarticleinfo";

        /// <summary>
        /// 获取专题列表
        /// </summary>
        public const string Subject_GetSubjectList = Host + "api/v2/subject-getsubjectlist";

        #endregion
    }
}
