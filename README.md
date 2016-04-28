# FileUpload

[![Join the chat at https://gitter.im/izhangzhihao/FileUpload](https://badges.gitter.im/izhangzhihao/FileUpload.svg)](https://gitter.im/izhangzhihao/FileUpload?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

# 文件上传，图片上传(后缀名验证，文件类型验证)，大文件分片上传，“秒传”，断点续传，传输失败自动重试，手动重试

|Master|Develop|
|:--:|:--:|
|[![Build status](https://ci.appveyor.com/api/projects/status/2t60n9j7p3i8gbd5/branch/master?svg=true)](https://ci.appveyor.com/project/izhangzhihao/fileupload/branch/master)|[![Build status](https://ci.appveyor.com/api/projects/status/2t60n9j7p3i8gbd5/branch/develop?svg=true)](https://ci.appveyor.com/project/izhangzhihao/fileupload/branch/develop)|

[![Issue Stats](http://issuestats.com/github/izhangzhihao/FileUpload/badge/pr)](http://issuestats.com/github/izhangzhihao/FileUpload)
[![Issue Stats](http://issuestats.com/github/izhangzhihao/FileUpload/badge/issue)](http://issuestats.com/github/izhangzhihao/FileUpload)

1.主要功能经测试支持IE9以上，Chrome，FireFox；其他浏览器未测试；

2.文件上传部分：主要实现了文件的上传，进度条，多文件一起上传，上传前删除，上传失败后手动删除，上传失败自动重试，上传失败手动重试（retry按钮），自动上传；

3.大文件上传部分：重磅功能：大文件“秒传”；在文件上传部分已有功能的基础上实现了按10MB分为多个块，异步上传，服务端合并，MD5验证，文件秒传，断点续传，网络问题自动重试，手动重试；

4.图片上传部分：在文件上传部分已有功能的基础上实现了上传前缩略图预览，前台js文件后缀验证，后台代码文件后缀验证和文件类型验证（就算修改后缀名也无法成功上传），支持图片上传前压缩；

5.单元测试部分：为每个Controller增加了单元测试，力求覆盖所有主要方法;

6.因为不会mock HttpPostedFileBase所以跳过两个测试，但是实际使用没有问题，所以就不管他了;
