var Austro=function()
{var austronaut=null;var _Austro=this;this.STOPPED=true;this.canvas=$('#canvasAustro');this.NAME='#canvasAustro';this.stage=new PIXI.Stage(0x000000);this.renderer=PIXI.autoDetectRenderer(this.canvas.width(),this.canvas.height(),this.canvas.get(0),true,true);var mission={};mission.x={};mission.x.min=25;mission.x.max=325;mission.y={};mission.y.min=380;mission.y.max=30;mission.scale={};mission.scale.min=0.6;mission.scale.max=1.2;mission.rotation={};mission.rotation.min=-0.9;mission.rotation.max=0.3;mission.phase=1;this.init=function(){astronaut=new PIXI.Sprite(PIXI.Texture.fromImage("austronaut.png"/*tpa=http://easyrocketstudio.com/js/min/img/austronaut.png*/));astronaut.position.x=mission.x.min;astronaut.position.y=mission.y.min;astronaut.scale.x=mission.scale.min;astronaut.scale.y=mission.scale.min;astronaut.rotation=mission.rotation.min;astronaut.pivot.set(50,50);_Austro.stage.addChild(astronaut)
_Austro.animate();}
this.animate=function(){requestAnimFrame(_Austro.animate);if(_Austro.STOPPED)
return;if(mission.phase==1)
{if(astronaut.position.x<mission.x.max)
astronaut.position.x+=0.1;if(astronaut.position.y>mission.y.max)
astronaut.position.y-=0.1;if(astronaut.scale.x<mission.scale.max)
{astronaut.scale.x+=0.0002;astronaut.scale.y+=0.0002;}
if(astronaut.rotation<mission.rotation.max)
astronaut.rotation+=0.0005;if(astronaut.rotation>mission.rotation.max)
mission.phase=0;}
if(mission.phase==0)
{if(astronaut.position.x>mission.x.min)
astronaut.position.x-=0.1;if(astronaut.position.y<mission.y.min)
astronaut.position.y+=0.1;if(astronaut.scale.x>mission.scale.min)
{astronaut.scale.x-=0.0002;astronaut.scale.y-=0.0002;}
if(astronaut.rotation>mission.rotation.min)
astronaut.rotation-=0.0005;if(astronaut.rotation<=mission.rotation.min)
mission.phase=1;}
_Austro.renderer.render(_Austro.stage);}
this.stop=function()
{_Austro.STOPPED=true;}
this.play=function()
{_Austro.STOPPED=false;}}
var A=new Austro;A.init();