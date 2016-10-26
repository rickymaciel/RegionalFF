
var theme = {
    color: [
        //shine
        '#c12e34', '#e6b600', '#0098d9', '#2b821d',
        '#005eaa', '#339ca8', '#cda819', '#32a487',
        //infographic
        '#C1232B','#B5C334','#FCCE10','#E87C25','#27727B',
        '#FE8463','#9BCA63','#FAD860','#F3A43B','#60C0DD',
        '#D7504B','#C6E579','#F4E001','#F0805A','#26C0C0',
        //dark
        '#FE8463','#9BCA63','#FAD860','#60C0DD','#0084C6',
        '#D7504B','#C6E579','#26C0C0','#F0805A','#F4E001',
        '#B5C334',
        //blue
        '#1790cf','#1bb2d8','#99d2dd','#88b0bb',
        '#1c7099','#038cc4','#75abd0','#afd6dd',
        //green
        '#408829','#68a54a','#a9cba2','#86b379',
        '#397b29','#8abb6f','#759c6a','#bfd3b7',
        //red
        '#d8361b','#f16b4c','#f7b4a9','#d26666',
        '#99311c', '#c42703', '#d07e75',
    ],

    title: {
        itemGap: 10,
        textStyle: {
            fontWeight: 'normal',
            color: '#4e2025'
        }
    },

    dataRange: {
        color: ['#1f610a', '#97b58d']
    },

    toolbox: {
        color: ['#408829', '#408829', '#408829', '#408829']
    },

    tooltip: {
        backgroundColor: 'rgba(0,0,0,0.5)',
        axisPointer: {
            type: 'line',
            lineStyle: {
                color: '#408829',
                type: 'dashed'
            },
            crossStyle: {
                color: '#408829'
            },
            shadowStyle: {
                color: 'rgba(200,200,200,0.3)'
            }
        }
    },
};

// 1
var echartPieesteaño = echarts.init(document.getElementById('esteaño'), theme);

echartPieesteaño.setOption({
    title : {
        text: TituloAño,
        subtext: '',
        x:'center'
    },
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        //orient: 'vertical',
        //left: 'left',
        x: 'center',
        y: 'bottom',
        data: LegendDataAño
    },
    toolbox: {
        show: true,
        feature: {
            mark: { show: true },
            //dataView: { show: true, readOnly: false },
            magicType: {
                show: true,
                type: ['pie', 'funnel'],
                option: {
                    funnel: {
                        x: '25%',
                        width: '50%',
                        funnelAlign: 'center',
                        max: 1548
                    }
                }
            },
            restore: {
                show: true,
                title: "Restaurar"
            },
            saveAsImage: {
                show: true,
                title: "Guardar Imagen"
            }
        }
    },
    calculable: true,
    series: [{
        name: TituloAño,
        type: 'pie',
        radius: '55%',
        center: ['50%', '48%'],
        data: SeriesDataAño,
        itemStyle: {
            emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
        }
    }]
});



// 2
var echartPieEstemes = echarts.init(document.getElementById('estemes'), theme);

echartPieEstemes.setOption({
    title: {
        text: TituloMes,
        subtext: '',
        x: 'center'
    },
    tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
    },
    legend: {
        //orient: 'vertical',
        //left: 'left',
        x: 'center',
        y: 'bottom',
        data: LegendDataMes
    },
    toolbox: {
        show: true,
        feature: {
            mark: { show: true },
            //dataView: { show: true, readOnly: false },
            magicType: {
                show: true,
                type: ['pie', 'funnel'],
                option: {
                    funnel: {
                        x: '25%',
                        width: '50%',
                        funnelAlign: 'center',
                        max: 1548
                    }
                }
            },
            restore: {
                show: true,
                title: "Restaurar"
            },
            saveAsImage: {
                show: true,
                title: "Guardar Imagen"
            }
        }
    },
    calculable: true,
    series: [{
        name: TituloMes,
        type: 'pie',
        radius: '55%',
        center: ['50%', '48%'],
        data: SeriesDataMes,
        itemStyle: {
            emphasis: {
                shadowBlur: 10,
                shadowOffsetX: 0,
                shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
        }
    }]
});



var dataStyle = {
    normal: {
        label: {
            show: false
        },
        labelLine: {
            show: false
        }
    }
};

var placeHolderStyle = {
    normal: {
        color: 'rgba(0,0,0,0)',
        label: {
            show: false
        },
        labelLine: {
            show: false
        }
    },
    emphasis: {
        color: 'rgba(0,0,0,0)'
    }
};



var echartMap = echarts.init(document.getElementById('echart_world_map'), theme);

echartMap.setOption({
    title: {
        text:  TituloMes,
        subtext: '',
        x: 'center',
        y: 'top'
    },
    tooltip: {
        trigger: 'item',
        formatter: function (params) {
            //var value = (params.value + '').split('.');
            //var value = parseInt(params.value);
            if (params.value > 0) {
                var value = parseInt(params.value);
            } else {
                var value = 0;
            }
            //value = value[0].replace(/(\d{1,3})(?=(?:\d{3})+(?!\d))/g, '$1,') + '.' + value[1];
            return params.seriesName + '<br/>' + params.name + ' : ' + value;
        }
    },
    toolbox: {
        show: true,
        orient: 'vertical',
        x: 'right',
        y: 'center',
        feature: {
            mark: {
                show: true
            },
            //dataView: {
            //    show: true,
            //    title: "Text View",
            //    lang: [
            //      "Text View",
            //      "Cerrar",
            //      "Refrescar",
            //    ],
            //    readOnly: false
            //},
            restore: {
                show: true,
                title: "Restaurar"
            },
            saveAsImage: {
                show: true,
                title: "Guardar Imagen"
            }
        }
    },
    dataRange: {
        min: 0,
        max: 10000,
        text: ['Alto', 'Bajo'],
        realtime: true,
        calculable: true,
        color: ['#087E65', '#0edeb2', '#9df9e5']
    },
    series: [{
        name: TituloMes,
        type: 'map',
        mapType: 'world',
        roam: true,
        mapLocation: {
            y: 60
        },
        itemStyle: {
            emphasis: {
                label: {
                    show: true
                }
            }
        },
        data: SeriesDataAño
    }]
});