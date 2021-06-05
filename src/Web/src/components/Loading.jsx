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
		<Lottie
			options={defaultOptions}
			height={400}
			width={400}
		/>
	);
}

export default Loading;