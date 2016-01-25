# FileUpload
文件上传，图片上传，大文件分片上传，文件“秒传”

AppVeyor:[![Build status](https://ci.appveyor.com/api/projects/status/2t60n9j7p3i8gbd5/branch/master?svg=true)](https://ci.appveyor.com/project/izhangzhihao/fileupload/branch/master)

主要基于jQuery,百度的webuploader，twitter的bootstrap；支持IE9以上，Chrome，FireFox,其他浏览器未测试；

百度团队官方的demo太渣了，直接复制下来都不能用...，webuploader地址：https://github.com/fex-team/webuploader;

文件上传部分：主要实现了文件的上传，进度条，多文件一起上传，上传前删除，上传失败自动重试，上传失败手动重试（retry按钮），自动上传；断点续传待加入！

大文件上传部分：重磅功能：大文件“秒传”；在文件上传部分已有功能的基础上实现了按10MB分为多个块，异步上传，服务端合并，MD5验证；断点续传待加入！

图片上传部分：在文件上传部分已有功能的基础上实现了上传前缩略图预览，前台js文件后缀验证，后台代码文件后缀验证和文件类型验证（就算修改后缀名也无法成功上传），支持图片上传前压缩；

单元测试部分：为每个Controller增加了单元测试，力求覆盖所有主要方法


存在的问题：断点续传暂时实现不了，单元测试不太熟悉，遇到了好几个问题解决不了
