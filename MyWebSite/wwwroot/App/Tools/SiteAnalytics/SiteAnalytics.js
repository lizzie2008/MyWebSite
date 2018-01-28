app.controller('SiteAnalyticsController', ['$scope', function ($scope) {
    //省份编码定义
    var dicProvince = {
        '江苏': 'CN-32',
        '贵州': 'CN-52',
        '云南': 'CN-53',
        '重庆': 'CN-50',
        '四川': 'CN-51',
        '上海': 'CN-31',
        '西藏': 'CN-54',
        '浙江': 'CN-33',
        '内蒙古': 'CN-15',
        '山西': 'CN-14',
        '福建': 'CN-16',
        '天津': 'CN-12',
        '河北': 'CN-13',
        '北京': 'CN-11',
        '安徽': 'CN-34',
        '江西': 'CN-36',
        '山东': 'CN-37',
        '河南': 'CN-41',
        '湖南': 'CN-43',
        '湖北': 'CN-42',
        '广西': 'CN-45',
        '广东': 'CN-44',
        '海南': 'CN-46',
        '新疆': 'CN-65',
        '宁夏': 'CN-64',
        '青海': 'CN-63',
        '甘肃': 'CN-62',
        '陕西': 'CN-61',
        '黑龙江': 'CN-23',
        '吉林': 'CN-22',
        '辽宁': 'CN-21'
    }

    var visitorsData = [];
    var pecentData = [];
    //请求区域分析数据
    var queryVisitDistrictAnalytics = function (startDate, endDate) {
        $('#dateRange').data('daterangepicker').setStartDate(startDate);
        $('#dateRange').data('daterangepicker').setEndDate(endDate);
        //请求百度数据
        $.ajax({
            type: 'GET',
            url: '/Tools/SiteAnalytics/GetVisitDistrictAnalytics'
                + '?' + 'startDate=' + startDate.format('YYYYMMDD')
                + '&' + 'endDate=' + endDate.format('YYYYMMDD'),
            success: function (data) {
                //获得站点数据
                var result = JSON.parse(data).body.data[0].result;

                visitorsData = [];
                pecentData = [];
                //统计表格数据
                var html = "";
                for (var i = 0; i < result.items[0].length; i++) {
                    var districtName = result.items[0][i][0].name;
                    var districtCode = dicProvince[districtName];
                    var districtData = result.items[1][i];
                    visitorsData[districtCode] = districtData[0];
                    pecentData[districtCode] = districtData[1];
                    html += '<tr>';
                    //序号
                    html += '<td>' + (i + 1) + '</td>';
                    //省份
                    html += '<td>' + districtName + '</td>';
                    //浏览量(PV)
                    html += '<td>' + districtData[0] + '</td>';
                    //占比
                    html += '<td><span class="badge bg-olive">' + districtData[1] + '%' + '</span></td>';
                    html += '</tr>';
                }
                //更新汇总指标
                $('#pv_count').html(result.pageSum[0][0]);
                $('#visitor_count').html(result.pageSum[0][3]);
                $('#ip_count').html(result.pageSum[0][6]);
                $('#bounce_ratio').html(result.pageSum[0][7] + "%");
                $('#avg_visit_time').html(result.pageSum[0][8] + " s");

                //初始化Map数据
                var options = {
                    map: 'cn_mill',
                    regionStyle: {
                        initial: {
                            fill: 'rgba(210, 214, 222, 1)',
                            'fill-opacity': 1,
                            stroke: 'none',
                            'stroke-width': 0,
                            'stroke-opacity': 1
                        },
                        hover: {
                            'fill-opacity': 0.7,
                            cursor: 'pointer'
                        },
                        selected: {
                            fill: 'yellow'
                        },
                        selectedHover: {}
                    },
                    series: {
                        regions: [
                            {
                                attribute: 'fill',
                                scale: ['#C8EEFF', '#0071A4'],
                                normalizeFunction: 'polynomial',
                                values: visitorsData,
                                legend: {
                                    horizontal: true
                                }
                            }
                        ]
                    },
                    onRegionTipShow: function (e, el, code) {
                        var html = '';
                        html += '<div style="width:120px;">';
                        html += '<div style="border-bottom:1px solid;padding-bottom:5px;">' + el.html() + '</div>';
                        html += '<div style="margin-top:5px;"><i class="fa fa-circle text-success margin-r-5"></i>浏览量(PV)<span class="pull-right">';
                        if (typeof visitorsData[code] != 'undefined') {
                            html += visitorsData[code];
                        } else {
                            html += 0;
                        }
                        html += '</div>';
                        html += '<div style="margin-top:5px;"><i class="fa fa-circle text-primary margin-r-5"></i>占比<span class="pull-right">';
                        if (typeof pecentData[code] != 'undefined') {
                            html += pecentData[code];
                        } else {
                            html += 0;
                        }
                        html += ' %</div>';
                        el.html(html);
                    }
                }
                $('#map-markers').empty();
                $('#map-markers').vectorMap(options);

                //刷新统计表格
                $('#districtTable tr:gt(0)').remove();
                $("#districtTable tbody").append(html);
            }
        });
    }
    //请求趋势分析数据
    var queryTrendAnalytics = function (startDate, endDate) {
        $.ajax({
            type: 'GET',
            url: '/Tools/SiteAnalytics/GetTrendAnalytics'
                + '?' + 'startDate=' + startDate.format('YYYYMMDD')
                + '&' + 'endDate=' + endDate.format('YYYYMMDD'),
            success: function (data) {
                //获得站点数据
                var result = JSON.parse(data).body.data[0].result;

                var arrPvCount = [];
                var arrVisitorCount = [];
                var arrIpCount = [];
                var arrAvgVisitTime = [];
                //构造柱状图
                for (var i = result.items[1].length - 1; i >= 0; i--) {
                    if (isNaN(parseInt(result.items[1][i][0]))) {
                        arrPvCount.push(0);
                    } else {
                        arrPvCount.push(result.items[1][i][0]);
                    }
                    if (isNaN(parseInt(result.items[1][i][3]))) {
                        arrVisitorCount.push(0);
                    } else {
                        arrVisitorCount.push(result.items[1][i][3]);
                    }
                    if (isNaN(parseInt(result.items[1][i][6]))) {
                        arrIpCount.push(0);
                    } else {
                        arrIpCount.push(result.items[1][i][6]);
                    }
                    if (isNaN(parseInt(result.items[1][i][8]))) {
                        arrAvgVisitTime.push(0);
                    } else {
                        arrAvgVisitTime.push(result.items[1][i][8]);
                    }
                }
                $('#trend_pv_count >div').html(arrPvCount.join(','));
                $('#trend_visitor_count >div').html(arrVisitorCount.join(','));
                $('#trend_ip_count >div').html(arrIpCount.join(','));
                $('#trend_avg_visit_time >div').html(arrAvgVisitTime.join(','));
                $('.sparkbar').each(function () {
                    var $this = $(this);
                    $this.sparkline('html', {
                        type: 'bar',
                        height: $this.data('height') ? $this.data('height') : '30',
                        barColor: $this.data('color')
                    });
                });
                //刷新趋势统计
                $('#trend_pv_count >h5').html(result.pageSum[0][0]);
                $('#trend_visitor_count >h5').html(result.pageSum[0][3]);
                $('#trend_ip_count >h5').html(result.pageSum[0][6]);
                $('#trend_avg_visit_time >h5').html(result.pageSum[0][8] + " s");
            }
        });
    }

    //时间范围选择
    $('#dateRange').daterangepicker({
        timePicker: false,
        locale: {
            format: 'YYYY-MM-DD',
            separator: ' 至 ',
            applyLabel: "确定",
            cancelLabel: "取消",
            resetLabel: "重置"
        }
    },
        function (startDate, endDate) {
            queryVisitDistrictAnalytics(startDate, endDate);
            $('input[type="radio"]').iCheck('uncheck');
        });

    //时间单选框
    $("input:radio[name='rdDateRange']").iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });
    $("input:radio[name='rdDateRange']").on('ifChecked', function () {
        var startDate = moment();
        var endDate = moment();
        switch ($(this).attr("id")) {
            case 'rdToday':     //今天
                startDate = moment();
                endDate = moment();
                break;
            case 'rdYesterday': //昨天
                startDate = moment().subtract(1, 'days');
                endDate = moment().subtract(1, 'days');
                break;
            case 'rdLast7Days': //最近7天
                startDate = moment().subtract(6, 'days');
                endDate = moment();
                break;
            case 'rdLast30Days'://最近30天
                startDate = moment().subtract(29, 'days');
                endDate = moment();
                break;
            default:
                console.log("单选框选择有误！");
        }
        queryVisitDistrictAnalytics(startDate, endDate);
    });
    //默认请求当天百度访客区域统计数据
    queryVisitDistrictAnalytics(moment().subtract(29, 'days'), moment());
    //默认请求7天内百度访客区域统计数据
    queryTrendAnalytics(moment().subtract(6, 'days'), moment());
}]);


