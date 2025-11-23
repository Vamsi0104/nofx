import React, { useEffect, useState } from 'react';
import { getPosts } from '../services/api';

interface Post {
    id: number;
    title: string;
    content: string;
}

const PostList = () => {
    const [posts, setPosts] = useState<Post[]>([]);

    useEffect(() => {
        getPosts().then(setPosts);
    }, []);

    return (
        <div>
            <h2 className="text-center">All Posts</h2>
            <ul className="list-unstyled">
                {posts.map((post) => (
                    <li key={post.id} className="mb-4">
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title">{post.title}</h5>
                                <p className="card-text">{post.content}</p>
                            </div>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PostList;