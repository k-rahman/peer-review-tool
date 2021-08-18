import React from 'react';
import Lottie from 'react-lottie';
import animationData from "../assets/lottie-files/36539-id-authentication.json";

const Loading = _ => {
	const defaultOptions = {
		loop: true,
		autoplay: true,
		animationData,
		rendererSettings: {
			preserveAspectRatio: "xMidYMid slice"
		}
	};

	return (
		<div style={{ minHeight: "calc(100vh - 64px - 48px - 61px - 18px)" }}>
			<Lottie
				options={defaultOptions}
				height={400}
				width={400}
			/>
		</div>
	);
}

export default Loading;