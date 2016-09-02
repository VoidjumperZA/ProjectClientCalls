Shader "ProdDesign/WaterShader" //This shader displays a red gradient
{
	Properties
	{
		_Texture("Main Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" } //enable when making transparent texture
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha //enable when making transparent texture

			CGPROGRAM

			//tell CG what functions to use
			#pragma vertex vert
			#pragma fragment frag

			uniform sampler2D _Texture; //global variable

			//from unity to shader
			struct appdata {
				float4 position : POSITION;  //position of vertex
				float4 normal : NORMAL; //store the normals
				float2 uv : TEXCOORD0; //store texture coordinates
			};

			//from vertex program to fragment program
			struct v2f {
				float4 position : SV_POSITION;
				float4 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			//project vertex onto screen
			v2f vert (appdata input) {
				//offset the vertices of your mesh
				//input.position.x = (input.position.x + input.position.y);
				
				//time(t / 20, t, t*2. t*3)
				input.position.y = sin(input.position.x + _Time.y) / 10; //change for wave height			

				v2f output;
				output.position = mul(UNITY_MATRIX_MVP, input.position);
				output.normal = input.normal;
				output.uv = input.uv;
				return output;
			}
			
			//calculate pixel color for any point on a triangle ('input' has varying coordinates)
			float4 frag (v2f input) : SV_Target
			{
				//from the sheets
				float4 colour = tex2D(_Texture, input.uv);
				colour.a = 0.6;
				return colour;
			}
			ENDCG
		}
	}
}
