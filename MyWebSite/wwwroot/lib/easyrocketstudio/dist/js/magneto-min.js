var Magneto=function()
{var _Magneto=this;this.STOPPED=true;this.canvas=$('#canvasMagneto');this.NAME='#canvasMagneto';this.stage=new PIXI.Stage(0x000000);this.renderer=PIXI.autoDetectRenderer(this.canvas.width(),this.canvas.height(),this.canvas.get(0),true,true);var white_circles=[];var yellow_circles=[];var blue_circles=[];var speed=0.007;var needed=50;var gr=new PIXI.Graphics();var radius=185;var s=2*Math.PI/180;this.init=function(){_Magneto.stage.addChild(gr)
_Magneto.animate();requestAnimFrame(_Magneto.animate);for(var i=0;i<needed;i++)
{white_circles[i]={'gStart':0,'f':getRandom(0,130),'R':getRandom(0.1,3),'x':0,'y':0,'default_x':getRandom(320,390),'default_y':getRandom(205,260),}}
for(var i=0;i<needed;i++)
{yellow_circles[i]={'gStart':0,'f':getRandom(0,130),'R':getRandom(1,4),'x':0,'y':0,'default_x':getRandom(320,390),'default_y':getRandom(210,240),}}
for(var i=0;i<needed;i++)
{blue_circles[i]={'gStart':0,'f':getRandom(0,130),'R':getRandom(1,2),'x':0,'y':0,'default_x':getRandom(320,390),'default_y':getRandom(210,240),}}}
this.animate=function(){requestAnimFrame(_Magneto.animate);if(_Magneto.STOPPED)
return;gr.clear();gr.beginFill(0xFFFFFF);for(var i in white_circles){white_circles[i].f-=speed;white_circles[i].x=white_circles[i].default_x+radius*Math.sin(white_circles[i].f);white_circles[i].y=white_circles[i].default_y+radius*Math.cos(white_circles[i].f);gr.drawCircle(white_circles[i].x,white_circles[i].y,white_circles[i].R);}
gr.beginFill(0xf9be2d);for(var i in yellow_circles){yellow_circles[i].f-=speed;yellow_circles[i].x=yellow_circles[i].default_x+radius*Math.sin(yellow_circles[i].f);yellow_circles[i].y=yellow_circles[i].default_y+radius*Math.cos(yellow_circles[i].f);gr.drawCircle(yellow_circles[i].x,yellow_circles[i].y,yellow_circles[i].R);}
gr.beginFill(0x13c1f4);for(var i in blue_circles){blue_circles[i].f-=speed;blue_circles[i].x=blue_circles[i].default_x+radius*Math.sin(blue_circles[i].f);blue_circles[i].y=blue_circles[i].default_y+radius*Math.cos(blue_circles[i].f);gr.drawCircle(blue_circles[i].x,blue_circles[i].y,blue_circles[i].R);}
gr.endFill();_Magneto.renderer.render(_Magneto.stage);}
this.stop=function()
{_Magneto.STOPPED=true;}
this.play=function()
{_Magneto.STOPPED=false;}}
function getRandom(min,max)
{return Math.round(Math.random()*(max-min)+min);}
function getRandomVariant(variants)
{return variants[getRandom(0,variants.length-1)];}
var M=new Magneto;if(!isMobile){M.init();}