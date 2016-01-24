# FileUpload
FileUpload/ImagUpload/BigFileUpload

主要基于jQuery,百度的webuploader，twitter的bootstrap；支持IE9以上，Chrome，FireFox,其他浏览器未测试；

百度团队官方的demo太渣了，直接复制下来都不能用...，webuploader地址：https://github.com/fex-team/webuploader;

文件上传部分：主要实现了文件的上传，进度条，多文件一起上传，上传前删除，自动上传；断点续传待加入！

大文件上传部分：重磅功能：大文件“秒传”；在文件上传部分已有功能的基础上实现了按10MB分为多个块，异步上传，服务端合并，MD5验证；断点续传待加入！

图片上传部分：在文件上传部分已有功能的基础上实现了上传前缩略图预览，前台js文件后缀验证，后台代码文件后缀验证和文件类型验证（就算修改后缀名也无法成功上传），支持图片上传前压缩；

单元测试部分：为每个Controller增加了单元测试，力求覆盖所有主要方法
