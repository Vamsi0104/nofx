import React, { useState, useEffect } from 'react';
import { createPost, getPosts } from './services/api';
import PostList from './components/PostList';

const App = () => {
    const [title, setTitle] = useState('');
    const [content, setContent] = useState('');

    const handleAddPost = async (e: React.FormEvent) => {
        e.preventDefault();
        if (title && content) {
            await createPost({ title, content });
            setTitle('');
            setContent('');
            // Reload posts after adding a new post
            getPosts().then((posts) => console.log(posts)); // You might want to update the state to show the new post
        }
    };

    return (
        <div className="container-fluid bg-dark text-white min-vh-100">
            <div className="row justify-content-center align-items-center" style={{ minHeight: '100vh' }}>
                <div className="col-12 col-md-8 col-lg-6">
                    <h1 className="mb-4 d-flex align-items-center justify-content-center">
                        
                        My Blog
                    </h1>

                    <div className="mb-5">
                        <h3>Create New Post</h3>
                        <form onSubmit={handleAddPost}>
                            <input
                                className="form-control mb-2"
                                placeholder="Title"
                                value={title}
                                onChange={(e) => setTitle(e.target.value)}
                            />
                            <textarea
                                className="form-control mb-2"
                                placeholder="Content"
                                value={content}
                                onChange={(e) => setContent(e.target.value)}
                            />
                            <button type="submit" className="btn btn-primary w-100">
                                Add Post
                            </button>
                        </form>
                    </div>

                    <hr className="bg-light" />
                    <PostList />
                </div>
            </div>
        </div>
    );
};

export default App;