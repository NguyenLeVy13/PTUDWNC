import React, { useEffect, useState } from "react";
import PostItem from '../Components/PostItem';
import { getPosts } from "../Servises/BlogRepository";

const Index = () => {
    const [postList, setPostList] = useState([]);

    useEffect(() => {
        document.title = 'Trang chủ';

        getPosts().then(data => {
            if (data) 
                setPostList(data.items);
            else
                setPostList([]);
        })
    }, []);

    if (postList.length > 0)
        return (
            <div className='p-4'>
                {postList.map(item => {
                    return (
                        <PostItem postItem={item} />
                    );
                })};
            </div>
        );
    else return (
        <></>
    );
}

export default Index;