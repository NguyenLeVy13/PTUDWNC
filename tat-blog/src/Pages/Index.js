import React, { useEffect } from "react";

const Index = () => {
    useEffect(() => {
        document.title = 'Trang chủ';
    }, []);

    return (
        <h1>
            Đây là trang chủ
        </h1>
    );
}

export default Index;