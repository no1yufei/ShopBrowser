


// 自动加载数据
function login() {
    var name = '%{name}%';
    var pwd = '%{password}%';
    var device_id = '%{device_id}%';
    var baseUrl = '%{url}%';
    var url = baseUrl + '/webchat/api/v1/sessions';

    var data = {
        device_id: device_id,
        password: pwd,
        username: name
    };


    var xhr = new XMLHttpRequest();
    xhr.open("POST", url, true);
    // 添加http头，发送信息至服务器时内容编码类型
    xhr.setRequestHeader("Content-Type", "text/plain;charset=UTF-8");

    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && (xhr.status == 200 || xhr.status == 304)) {
            window.location = baseUrl + "/webchat/conversations";
        }
        else if (xhr.status != 200) {
            confirm('登失败' + xhr.readyState + xhr.status);
        }
    };
    xhr.send(JSON.stringify(data));

}
function order() {
    var baseUrl = '%{url}%';
    window.location = baseUrl + "/portal/sale";
}
order();
login();

function getCookie(name) {
    var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");

    if (arr = document.cookie.match(reg))

        return unescape(arr[2]);
    else
        return null;
}
interaction.setResult(getCookie('%{name}%'));



